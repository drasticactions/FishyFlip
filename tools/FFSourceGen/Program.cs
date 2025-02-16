// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using System.Text.Json;
using ConsoleAppFramework;
using FFSourceGen;
using FFSourceGen.Models;

var app = ConsoleApp.Create();
app.Add<AppCommands>();
app.Run(args);

/// <summary>
/// App Commands.
/// </summary>
#pragma warning disable SA1649 // File name should match first type name
public partial class AppCommands
#pragma warning restore SA1649 // File name should match first type name
{
    public static string baseNamespace { get; set; } = "FishyFlip.Lexicon";
    private string basePath { get; set; }

    public static List<ClassGeneration> AllClasses { get; } = new();

    public static IOrderedEnumerable<ClassGeneration> AllClassesSored => AllClasses.OrderBy(n => n.ClassName);

    /// <summary>
    /// Generate lexicon source code.
    /// </summary>
    /// <param name="lexiconPath">Path to lexicon files.</param>
    /// <param name="outputDir">-o, Output directory.</param>
    /// <param name="baseNamespace">-n, Base Namespace.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <param name="thirdPartyDirs">-t, Third Party Directories.</param>
    /// <returns>Task.</returns>
    [Command("generate")]
    public async Task GenerateAsync([Argument] string lexiconPath, string? outputDir = default,
        string? baseNamespace = default, CancellationToken cancellationToken = default, params string[] thirdPartyDirs)
    {
        baseNamespace = baseNamespace ??= baseNamespace;
        outputDir ??= AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine($"Lexicon Path: {lexiconPath}");
        Console.WriteLine($"Output Directory: {outputDir}");

        this.basePath = Path.Combine(outputDir, "Lexicon");
        if (Directory.Exists(this.basePath))
        {
            Directory.Delete(this.basePath, true);
        }

        Directory.CreateDirectory(this.basePath);

        await this.GenerateClasses(lexiconPath);

        foreach (var dir in thirdPartyDirs)
        {
            var directories = GetHighestLevelDirectories(dir);
            foreach (var directory in directories.Where(n => !n.Contains("bsky") && !n.Contains("atproto")))
            {
                await this.GenerateClasses(directory);
            }
        }

        await this.GenerateModelFiles(lexiconPath);
    }

    private List<string> GetHighestLevelDirectories(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));
        }

        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"The specified path does not exist: {path}");
        }

        // Initialize the list to store top-level directories
        var highestLevelDirectories = new List<string>();

        // Use a stack for Depth-First Search (DFS) traversal
        var directoriesToProcess = new Stack<string>();
        directoriesToProcess.Push(path);

        while (directoriesToProcess.Count > 0)
        {
            var currentDirectory = directoriesToProcess.Pop();
            var subdirectories = Directory.GetDirectories(currentDirectory);

            // If no subdirectories exist, the current directory is a "highest level" directory
            if (subdirectories.Length == 0)
            {
                highestLevelDirectories.Add(currentDirectory);
            }
            else
            {
                // Add subdirectories to the stack for further processing
                foreach (var subdirectory in subdirectories)
                {
                    directoriesToProcess.Push(subdirectory);
                }
            }
        }

        return highestLevelDirectories;
    }

    private async Task GenerateClasses(string lexiconPath)
    {
        var files = Directory.EnumerateFiles(lexiconPath, "*.json", SearchOption.AllDirectories).OrderBy(n => n).ToList();
        Console.WriteLine($"Found {files.Count()} files.");

        var defs = files.Where(n => n.Contains("defs.json")).ToList();
        var mainRecordFiles = this.GetMainRecordJsonFiles(files.Except(defs).ToList());
        var other = files.Except(defs).Except(mainRecordFiles).ToList();

        foreach (var jsonFile in defs)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            await this.ProcessFile(jsonFile);
        }

        foreach (var jsonFile in mainRecordFiles)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            await this.ProcessFile(jsonFile);
        }

        foreach (var jsonFile in other)
        {
            var filename = Path.GetFileNameWithoutExtension(jsonFile);
            await this.ProcessFile(jsonFile);
        }
    }

    private async Task GenerateModelFiles(string lexiconPath)
    {
        foreach (var cls in AllClassesSored)
        {
            switch (cls.Definition.Type)
            {
                case "object":
                case "record":
                    await this.GenerateModelFile(cls);
                    break;
                default:
                    Console.WriteLine($"Skipping {cls.Definition.Type} for {cls.Id}");
                    break;
            }
        }

        var modelClasses = AllClassesSored.Where(n => n.Definition.Type == "object" || n.Definition.Type == "record")
            .ToList();
        var procedureClasses = AllClassesSored
            .Where(n => n.Definition.Type == "procedure" || n.Definition.Type == "query")
            .GroupBy(n => n.CSharpNamespace).ToList();
        foreach (var group in procedureClasses)
        {
            await this.GenerateEndpointGroupAsync(group);
            await this.GenerateEndpointClassAsync(group);
        }

        var cursorBasedClasses = AllClassesSored.Where(n => n.IsOutput && n.HasCursor && n.HasOutputList);
        foreach (var cls in cursorBasedClasses)
        {
            await this.GenerateCursorBasedCollectionClassAsync(cls);
        }

        await this.GenerateEndpointClassListForATProtoCS(procedureClasses);

        await this.GenerateJsonSerializerContextFile(modelClasses);
        await this.GenerateCBORToATObjectConverterClassFile(modelClasses);

        var atRecordSource = this.GenerateATObjectSource(baseNamespace, modelClasses);
        var atRecordPath = Path.Combine(this.basePath, "ATObject.g.cs");
        await File.WriteAllTextAsync(atRecordPath, atRecordSource);

        var stringClassWithKnownValues = AllClasses
            .Where(n => n.Definition.Type == "string" && (n.Definition.KnownValues?.Any() ?? false)).ToList();
        foreach (var cls in stringClassWithKnownValues)
        {
            await this.GenerateConstantKnownValueClass(cls);
        }

        var records = AllClassesSored.Where(n => n.Definition.Type == "record").GroupBy(n => n.CSharpNamespace)
            .ToList();
        foreach (var record in records)
        {
            await this.GenerateRecordStaticExtensionClass(record);
            await this.GenerateRecordExtensionClass(record);
        }

        var errors = AllClassesSored.SelectMany(n => n.Definition.Errors ?? Array.Empty<ErrorDefinition>()).Select(n => n.Name).Distinct().ToList();
        await this.GenerateATErrorClasses(errors);
        await this.GenerateATErrorObjects(errors);
    }

    private async Task GenerateConstantKnownValueClass(ClassGeneration cls)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{baseNamespace}.{cls.CSharpNamespace}");
        sb.AppendLine("{");
        sb.AppendLine();
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Known Values for {cls.ClassName}.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class {cls.ClassName}");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var knownValue in cls.Definition.KnownValues!)
        {
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {knownValue}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine(
                $"        public const string {knownValue.Split("#").Last().ToClassSafe().ToPascalCase()} = \"{knownValue}\";");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, cls.CSharpNamespace.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{cls.ClassName}.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: {cls.ClassName} {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private async Task GenerateEndpointClassListForATProtoCS(List<IGrouping<string, ClassGeneration>> groupList)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"FishyFlip");
        sb.AppendLine("{");
        sb.AppendLine();
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// ATProto Endpoint List.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public sealed partial class ATProtocol");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var group in groupList.OrderBy(n => n.Key))
        {
            var groupSplit = group.Key.Split(".");
            string className;
            if (groupSplit[1] == "Atproto")
            {
                className = $"ATProto{groupSplit.Last().ToPascalCase()}";
            }
            else if (groupSplit[0] == "App")
            {
                className = $"Bluesky{groupSplit.Last().ToPascalCase()}";
            }
            else
            {
                className = string.Join(string.Empty, groupSplit.Select(n => n.ToPascalCase()).ToArray());
            }

            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {group.Key.ToLower()} Endpoint Group.");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine(
                $"        public {baseNamespace}.{group.Key}.{className} {className.Replace("ATProto", string.Empty).Replace("Bluesky", string.Empty)} => new (this);");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath);
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"ATProtocol.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: ATProtocol {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private async Task GenerateEndpointClassAsync(IGrouping<string, ClassGeneration> group)
    {
        Console.WriteLine($"Generating Endpoint Group: {group.Key}");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{baseNamespace}.{group.Key}");
        var groupSplit = group.Key.Split(".");
        string className;
        if (groupSplit[1] == "Atproto")
        {
            className = $"ATProto{groupSplit.Last().ToPascalCase()}";
        }
        else if (groupSplit[0] == "App")
        {
            className = $"Bluesky{groupSplit.Last().ToPascalCase()}";
        }
        else
        {
            className = string.Join(string.Empty, groupSplit.Select(n => n.ToPascalCase()).ToArray());
        }

        Console.WriteLine($"Generated Class Name: {className}");
        sb.AppendLine("{");
        sb.AppendLine();
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// {group.Key.ToLower()} Endpoint Class.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public sealed class {className}");
        sb.AppendLine("    {");
        sb.AppendLine();
        sb.AppendLine("        private ATProtocol atp;");
        sb.AppendLine();
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{className}\"/> class.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        /// <param name=\"atp\"><see cref=\"ATProtocol\"/>.</param>");
        sb.AppendLine($"        internal {className}(ATProtocol atp)");
        sb.AppendLine("        {");
        sb.AppendLine("            this.atp = atp;");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine("        /// Gets the ATProtocol.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine("        internal ATProtocol ATProtocol => this.atp;");
        sb.AppendLine();
        foreach (var item in group)
        {
            sb.AppendLine();
            Console.WriteLine($"{item.Definition.Type} {item.Id}");
            var methodName = $"{item.ClassName}Async";
            var (requiredProperties, optionalProperties) = this.FetchInputProperties(item);
            var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            var outputProperty = this.FetchOutputProperties(item);
            var sourceContext = outputProperty == "Success"
                ? "Success"
                : $"{item.CSharpNamespace.Replace(".", string.Empty)}{outputProperty.Split(".").Last()}";
            var description = !string.IsNullOrEmpty(item.Definition.Description)
                ? item.Definition.Description
                : $"Generated endpoint for {item.Id}";
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {description}");
            this.GenerateErrorConstructorDocs(sb, item.Definition.Errors);
            sb.AppendLine($"        /// </summary>");
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public Task<Result<{outputProperty}?>> {methodName} (");
            for (int i = 0; i < inputProperties.Count; i++)
            {
                sb.Append($"{inputProperties[i]}");
                if (i < inputProperties.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.AppendLine($")");
            sb.AppendLine("        {");
            if (item.Definition.RequiresAuth)
            {
                // this.CreateAuthenticationCheck(sb);
            }

            sb.Append($"            return atp.{methodName}(");
            for (int i = 0; i < inputProperties.Count; i++)
            {
                sb.Append($"{inputProperties[i].Split(" ")[1]}");
                if (i < inputProperties.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.AppendLine($");");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, group.Key.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{className}.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: {className} {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private async Task GenerateRecordExtensionClass(IGrouping<string, ClassGeneration> group)
    {
        var outputPath = Path.Combine(this.basePath, group.Key.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);

        var classPath = Path.Combine(outputPath, $"{group.Key.Split(".").Last()}Extensions.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: {group.Key.Split(".").Last()}Extensions {classPath}");
        }

        var groupSplit = group.Key.Split(".");
        var className = string.Empty;
        if (groupSplit[1] == "Atproto")
        {
            className = $"ATProto{groupSplit.Last().ToPascalCase()}";
        }
        else if (groupSplit[0] == "App")
        {
            className = $"Bluesky{groupSplit.Last().ToPascalCase()}";
        }
        else
        {
            className = string.Join(string.Empty, groupSplit.Select(n => n.ToPascalCase()).ToArray());
        }

        if (!File.Exists(Path.Combine(outputPath, $"{className}.g.cs")))
        {
            Console.WriteLine($"Skipping Record Extension Class: {group.Key}");
            return;
        }

        var createRecord = AllClasses.First(n => n.Id == "com.atproto.repo.createRecord");
        var deleteRecord = AllClasses.First(n => n.Id == "com.atproto.repo.deleteRecord");
        var putRecord = AllClasses.First(n => n.Id == "com.atproto.repo.putRecord");
        var getRecord = AllClasses.First(n => n.Id == "com.atproto.repo.getRecord");
        var listRecords = AllClasses.First(n => n.Id == "com.atproto.repo.listRecords");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        sb.AppendLine($"using FishyFlip.Lexicon.Com.Atproto.Repo;");
        sb.AppendLine();
        this.GenerateNamespace(sb, baseNamespace);
        sb.AppendLine("{");
        sb.AppendLine();
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Extension methods for {group.Key.ToLower()}.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class {className}Extensions");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var item in group)
        {
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Create a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            var (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord, true);
            var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            inputProperties[inputProperties.IndexOf("ATObject record")] =
                $"{baseNamespace}.{item.FullClassName} record";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<CreateRecordOutput?>> Create{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            var properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            // replace collection with the first input property.
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.CreateRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Create a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord, false);
            requiredProperties.Remove(requiredProperties.First(n => n == "string collection"));
            requiredProperties.Remove(requiredProperties.First(n => n == "ATObject record"));
            requiredProperties.Remove(requiredProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));

            var (propertyRequiredProperties, propertyOptionalProperties) =
                this.FetchInputPropertiesFromProperties(item, true);
            propertyOptionalProperties.RemoveAt(propertyOptionalProperties.Count - 1);
            var totalRequiredProperties = propertyRequiredProperties.Concat(requiredProperties).ToList();
            var totalOptionalProperties = propertyOptionalProperties.Concat(optionalProperties).ToList();
            inputProperties = totalRequiredProperties.Concat(totalOptionalProperties).ToList();
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<CreateRecordOutput?>> Create{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            var record = new {baseNamespace}.{item.FullClassName}();");
            for (int i = 0; i < item.Properties.Count(); i++)
            {
                var prop = item.Properties[i].PropertyName;
                var key = item.Properties[i].Key;
                if (key == "createdAt")
                {
                    sb.AppendLine($"            record.{prop} = {this.PropertyNameToCSharpSafeValue(key)} ?? DateTime.UtcNow;");
                }
                else
                {
                    sb.AppendLine($"            record.{prop} = {this.PropertyNameToCSharpSafeValue(key)};");
                }
            }

            sb.AppendLine($"            return atp.ATProtocol.CreateRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Delete a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(deleteRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<DeleteRecordOutput?>> Delete{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(deleteRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.DeleteRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Put a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(putRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            inputProperties[inputProperties.IndexOf("ATObject record")] =
                $"{baseNamespace}.{item.FullClassName} record";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<PutRecordOutput?>> Put{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(putRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            // replace collection with the first input property.
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.PutRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// List {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<ListRecordsOutput?>> List{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.ListRecordsAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// List {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<ListRecordsOutput?>> List{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.ListRecordsAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Get {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<GetRecordOutput?>> Get{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.ATProtocol.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.GetRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Get {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties[0] = $"this {baseNamespace}.{item.CSharpNamespace}.{className} atp";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<GetRecordOutput?>> Get{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ATProtocol.GetRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private async Task GenerateRecordStaticExtensionClass(IGrouping<string, ClassGeneration> group)
    {
        var createRecord = AllClasses.First(n => n.Id == "com.atproto.repo.createRecord");
        var deleteRecord = AllClasses.First(n => n.Id == "com.atproto.repo.deleteRecord");
        var putRecord = AllClasses.First(n => n.Id == "com.atproto.repo.putRecord");
        var listRecords = AllClasses.First(n => n.Id == "com.atproto.repo.listRecords");
        var getRecord = AllClasses.First(n => n.Id == "com.atproto.repo.getRecord");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        sb.AppendLine($"using FishyFlip.Lexicon.Com.Atproto.Repo;");
        sb.AppendLine();
        this.GenerateNamespace(sb, $"{baseNamespace}.{group.Key}");
        sb.AppendLine("{");
        sb.AppendLine();
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Extension methods for {group.Key.ToLower()}.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class ATProto{group.Key.Split(".").Last()}Extensions");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var item in group)
        {
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Create a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            var (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord, true);
            var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties[inputProperties.IndexOf("ATObject record")] = $"{baseNamespace}.{item.FullClassName} record";
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<CreateRecordOutput?>> Create{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            var properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            // replace collection with the first input property.
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.CreateRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Create a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord, false);
            requiredProperties.Remove(requiredProperties.First(n => n == "string collection"));
            requiredProperties.Remove(requiredProperties.First(n => n == "ATObject record"));
            requiredProperties.Remove(requiredProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<CreateRecordOutput?>> Create{item.ClassName}Async(");
            var (propertyRequiredProperties, propertyOptionalProperties) =
                this.FetchInputPropertiesFromProperties(item, true);
            propertyOptionalProperties.RemoveAt(propertyOptionalProperties.Count - 1);
            var totalRequiredProperties = propertyRequiredProperties.Concat(requiredProperties).ToList();
            var totalOptionalProperties = propertyOptionalProperties.Concat(optionalProperties).ToList();
            this.GenerateInputProperties(sb, totalRequiredProperties.Concat(totalOptionalProperties).ToList());
            (requiredProperties, optionalProperties) = this.FetchInputProperties(createRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            var record = new {baseNamespace}.{item.FullClassName}();");
            for (int i = 0; i < item.Properties.Count(); i++)
            {
                var prop = item.Properties[i].PropertyName;
                var key = item.Properties[i].Key;
                if (key == "createdAt")
                {
                    sb.AppendLine($"            record.{prop} = {this.PropertyNameToCSharpSafeValue(key)} ?? DateTime.UtcNow;");
                }
                else
                {
                    sb.AppendLine($"            record.{prop} = {this.PropertyNameToCSharpSafeValue(key)};");
                }
            }

            sb.AppendLine($"            return atp.CreateRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Delete a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(deleteRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<DeleteRecordOutput?>> Delete{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(deleteRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.DeleteRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Put a {item.ClassName} record.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(putRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties[inputProperties.IndexOf("ATObject record")] = $"{baseNamespace}.{item.FullClassName} record";
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<PutRecordOutput?>> Put{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(putRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            // replace collection with the first input property.
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.PutRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// List {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<ListRecordsOutput?>> List{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ListRecordsAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// List {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<ListRecordsOutput?>> List{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(listRecords);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.ListRecordsAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");

            // Get Record
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Get {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<GetRecordOutput?>> Get{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.GetRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine($"        /// Get {item.ClassName} records.");
            sb.AppendLine("        /// </summary>");
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord, true);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            inputProperties.Remove(inputProperties.First(n => n == "string collection"));
            inputProperties.Remove(inputProperties.First(n => n == "FishyFlip.Models.ATIdentifier repo"));
            this.GenerateParams(sb, item, inputProperties);
            sb.Append($"        public static Task<Result<GetRecordOutput?>> Get{item.ClassName}Async(");
            this.GenerateInputProperties(sb, inputProperties);
            (requiredProperties, optionalProperties) = this.FetchInputProperties(getRecord);
            inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
            properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
            properties[properties.IndexOf("repo")] =
                $"atp.SessionManager.Session?.Did ?? throw new InvalidOperationException(\"Session did is required.\")";
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine($"            return atp.GetRecordAsync({string.Join(", ", properties)});");
            sb.AppendLine("        }");

        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, group.Key.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"ATProto{group.Key.Split(".").Last()}Extensions.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: ATProto{group.Key.Split(".").Last()}Extensions {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private void GenerateInputProperties(StringBuilder sb, List<string> inputProperties, bool removeStatic = false, bool removeNullable = false)
    {
        var start = removeStatic ? 1 : 0;
        for (int i = start; i < inputProperties.Count; i++)
        {
            var value = removeNullable ? inputProperties[i].Replace("?", string.Empty) : inputProperties[i];
            sb.Append($"{value}");
            if (i < inputProperties.Count - 1)
            {
                sb.Append(", ");
            }
        }
    }

    private void GenerateParams(StringBuilder sb, ClassGeneration item, List<string> inputProperties)
    {
        foreach (var prop2 in inputProperties)
        {
            var testName = prop2.Replace("this ", string.Empty).Split(" ")[1];
            sb.Append($"        /// <param name=\"{testName}\">");
            var propName = testName.Replace("@", string.Empty);
            var prop = item.Properties.FirstOrDefault(n => n.Key == propName);

            if (prop is null && (item.Definition.Input?.Schema?.Properties.Any() ?? false))
            {
                var prop3 = item.Definition.Input.Schema.Properties.FirstOrDefault(n => n.Key == propName);
                if (prop3.Value?.Ref is not null)
                {
                    var cls = FindClassFromRef(prop3.Value.Ref);
                    if (cls is not null)
                    {
                        sb.Append(cls.Definition.Description ?? string.Empty);

                        if (cls.Definition.Type == "union" ||
                            (cls.Definition.Type == "array" && cls.Definition.Items?.Type == "union"))
                        {
                            sb.AppendLine();
                            sb.AppendLine($"        /// <br/> Union Types: <br/>");
                            var refs = cls.Definition.Refs ?? cls.Definition.Items?.Refs;
                            foreach (var unionType in refs!)
                            {
                                var refString = unionType.Contains("#")
                                    ? $"{item.Namespace}.defs#{unionType.Split("#")[1]}"
                                    : unionType;
                                var cls2 = FindClassFromRef(refString);
                                sb.AppendLine(cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object"
                                    ? $"        /// <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString}) <br/>"
                                    : $"        /// {unionType} <br/>");
                            }

                            sb.AppendLine("        /// </param>");
                        }
                        else if (cls.Definition.KnownValues?.Any() ?? false)
                        {
                            sb.AppendLine();
                            sb.AppendLine($"        /// <br/> Known Values: <br/>");
                            foreach (var knownValue in cls.Definition.KnownValues)
                            {
                                var refString = knownValue.Contains("#")
                                    ? $"{item.Id}#{knownValue.Split("#")[1]}"
                                    : knownValue;
                                var cls2 = FindClassFromRef(refString);
                                sb.AppendLine(cls2 is not null
                                    ? $"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description} <br/>"
                                    : $"        /// {knownValue.Split("#").Last()} <br/>");
                            }

                            sb.AppendLine("        /// </param>");
                        }
                        else
                        {
                            sb.AppendLine("</param>");
                        }
                    }
                    else
                    {
                        sb.AppendLine("</param>");
                    }
                }
                else
                {
                    sb.AppendLine("</param>");
                }

                continue;
            }

            if (prop is null)
            {
                sb.AppendLine("</param>");
                continue;
            }

            sb.Append(prop.PropertyDefinition.Description ?? string.Empty);

            if (prop.PropertyDefinition.Type == "ref")
            {
                sb.AppendLine();
                var refString = prop.PropertyDefinition.Ref?.Contains("#") ?? false
                    ? $"{item.Namespace}.defs#{prop.PropertyDefinition.Ref.Split("#")[1]}"
                    : prop.PropertyDefinition.Ref;
                var cls2 = FindClassFromRef(refString ?? string.Empty);
                if (cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object")
                {
                    sb.AppendLine(
                        $"        /// <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
                }
                else if (cls2?.Definition.KnownValues?.Any() ?? false)
                {
                    sb.AppendLine($"        /// <br/> Known Values: <br/>");
                    foreach (var knownValue in cls2.Definition.KnownValues)
                    {
                        var refString2 = knownValue.Contains("#")
                            ? $"{cls2.Id}#{knownValue.Split("#")[1]}"
                            : knownValue;
                        var cls3 = FindClassFromRef(refString2);
                        sb.AppendLine(cls3 is not null
                            ? $"        /// {knownValue} - {cls3.Definition.Description} <br/>"
                            : $"        /// {knownValue} <br/>");
                    }
                }
                else
                {
                    sb.AppendLine($"        /// {refString}");
                }

                sb.AppendLine("        /// </param>");
                continue;
            }

            if (prop.PropertyDefinition.Type == "union" || (prop.PropertyDefinition.Type == "array" &&
                                                            prop.PropertyDefinition.Items?.Type == "union"))
            {
                sb.AppendLine();
                sb.AppendLine($"        /// <br/> Union Types: <br/>");
                var refs = prop.PropertyDefinition.Refs ?? prop.PropertyDefinition.Items?.Refs;
                foreach (var unionType in refs!)
                {
                    var refString = unionType.Contains("#")
                        ? $"{item.Namespace}.defs#{unionType.Split("#")[1]}"
                        : unionType;
                    var cls2 = FindClassFromRef(refString);
                    sb.AppendLine(cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object"
                        ? $"        /// <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString}) <br/>"
                        : $"        /// {unionType} <br/>");
                }

                sb.AppendLine("        /// </param>");
            }
            else if (prop.PropertyDefinition.KnownValues?.Any() ?? false)
            {
                sb.AppendLine();
                sb.AppendLine($"        /// <br/> Known Values: <br/>");
                foreach (var knownValue in prop.PropertyDefinition.KnownValues)
                {
                    var refString = knownValue.Contains("#") ? $"{item.Id}#{knownValue.Split("#")[1]}" : knownValue;
                    var cls2 = FindClassFromRef(refString);
                    sb.AppendLine(cls2 is not null
                        ? $"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description} <br/>"
                        : $"        /// {knownValue.Split("#").Last()} <br/>");
                }

                sb.AppendLine("        /// </param>");
            }
            else
            {
                sb.AppendLine("</param>");
            }
        }
    }

    private void GenerateErrorConstructorDocs(StringBuilder sb, ErrorDefinition[]? errors)
    {
        if (errors is null || !errors.Any())
        {
            return;
        }

        sb.AppendLine("        /// <br/> Possible Errors: <br/>");
        foreach (var error in errors)
        {
            sb.AppendLine($"        /// <see cref=\"{baseNamespace}.{error.Name.ToPascalCase()}Error\"/> {error.Description} <br/>");
        }
    }
    
    private async Task GenerateCursorBasedCollectionClassAsync(ClassGeneration cls)
    {
        Console.WriteLine($"Generating Collection Item: {cls.Key}");
        var inputClassName = cls.ClassName.Replace("Output", string.Empty);
        var inputClass = AllClasses.FirstOrDefault(n => n.ClassName == inputClassName && n.CSharpNamespace == cls.CSharpNamespace);
        if (inputClass is null)
        {
            throw new Exception($"Could not find input class for {cls.ClassName}");
        }

        if (cls.OutputList is null)
        {
            throw new Exception($"OutputList is null for {cls.ClassName}");
        }

        // Remove List<> wrapper from type name
        var rawType = cls.OutputList.Type.Split("<").Last().Split(">").First();

        if (cls.OutputList.IsBaseType)
        {
            return;
        }

        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{baseNamespace}.{cls.CSharpNamespace}");
        var className = $"{cls.ClassName}Collection";
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// {cls.ClassName} Collection.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public class {className} : ATObjectCollectionBase<{rawType}>, IAsyncEnumerable<{rawType}>");
        sb.AppendLine("    {");
        sb.AppendLine();
        // Generate the input properties
        var (requiredProperties, optionalProperties) = this.FetchInputProperties(inputClass);
        // Add ATProtocol to Required
        requiredProperties.Insert(0, "FishyFlip.ATProtocol atp");
        var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
        //this.GenerateParams(sb, cls, inputProperties);
        sb.Append($"        public {className}(");
        this.GenerateInputProperties(sb, inputProperties, false, true);
        sb.AppendLine(")");
        sb.AppendLine("             : base(atp)");
        sb.AppendLine("        {");
        for (int i = 1; i < inputProperties.Count; i++)
        {
            var typeName = inputProperties[i].Split(" ")[0];
            var prop = inputProperties[i].Split(" ")[1];
            sb.AppendLine($"            this.{prop.ToPascalCase()} = {PropertyNameToCSharpSafeValue(prop)};");
        }

        sb.AppendLine("        }");
        sb.AppendLine();
        // Generate getters for the properties except for ATProtocol, Limit, Cursor, and CancellationToken.
        for (int i = 1; i < inputProperties.Count; i++)
        {
            var typeName = inputProperties[i].Split(" ")[0];
            var prop = inputProperties[i].Split(" ")[1];
            if (prop == "limit" || prop == "cursor" || prop == "cancellationToken")
            {
                continue;
            }

            // sb.AppendLine($"        /// <summary>");
            // sb.AppendLine($"        /// {prop}.");
            // sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        public {typeName} {prop.ToPascalCase()} {{ get; }}");
            sb.AppendLine();
        }

        sb.AppendLine($"        /// <inheritdoc/>");
        sb.AppendLine($"        public override async Task<(IList<{rawType}> Posts, string Cursor)> GetRecordsAsync(int? limit = null, CancellationToken? token = default)");
        sb.AppendLine("        {");
        sb.AppendLine($"            token = token ?? this.CancellationToken ?? System.Threading.CancellationToken.None;");
        sb.Append($"            var (result, error) = await this.ATProtocol.{inputClass.ClassName}Async(");
        for (int i = 1; i < inputProperties.Count; i++)
        {
            var typeName = inputProperties[i].Split(" ")[0];
            var prop = inputProperties[i].Split(" ")[1];
            if (prop == "limit" || prop == "cursor" || prop == "cancellationToken")
            {
                continue;
            }

            sb.Append($"{prop}: this.{prop.ToPascalCase()}, ");
        }
        if (inputProperties.Any(n => n.Contains("limit")))
        {
            sb.Append("limit: limit, ");
        }
        sb.AppendLine("cursor: this.Cursor, cancellationToken: token.Value!);");
        sb.AppendLine();
        sb.AppendLine("            this.HandleATError(error);");
        sb.AppendLine();
        sb.AppendLine($"            if (result == null || result.{cls.OutputList.PropertyName} == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                throw new InvalidOperationException(\"The result or its properties cannot be null.\");");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine($"            return (result.{cls.OutputList.PropertyName}, result.Cursor ?? string.Empty);");
        sb.AppendLine("        }");

        sb.AppendLine("    }");
        sb.AppendLine("}");

        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, cls.CSharpNamespace.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{className}.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: {className} {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private async Task GenerateEndpointGroupAsync(IGrouping<string, ClassGeneration> group)
    {
        Console.WriteLine($"Generating Endpoint Group: {group.Key}");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{baseNamespace}.{group.Key}");
        var className = $"{group.Key.Split(".").Last()}Endpoints";
        sb.AppendLine("{");
        sb.AppendLine();
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// {group.Key.ToLower()} Endpoint Group.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class {className}");
        sb.AppendLine("    {");
        sb.AppendLine();
        foreach (var item in group)
        {
            sb.AppendLine($"       public const string {item.ClassName} = \"/xrpc/{item.Id}\";");
            sb.AppendLine();
        }

        foreach (var item in group)
        {
            sb.AppendLine();
            Console.WriteLine($"{item.Definition.Type} {item.Id}");
            var methodName = $"{item.ClassName}Async";
            var (requiredProperties, optionalProperties) = this.FetchInputProperties(item, true);
            var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
            var outputProperty = this.FetchOutputProperties(item);
            var sourceContext = outputProperty == "Success"
                ? "Success"
                : $"{item.CSharpNamespace.Replace(".", string.Empty)}{outputProperty.Split(".").Last()}";
            var description = !string.IsNullOrEmpty(item.Definition.Description)
                ? item.Definition.Description
                : $"Generated endpoint for {item.Id}";
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {description}");
            this.GenerateErrorConstructorDocs(sb, item.Definition.Errors);
            sb.AppendLine($"        /// </summary>");
            this.GenerateParams(sb, item, inputProperties);
            sb.AppendLine($"        /// <returns>Result of <see cref=\"{outputProperty}?\"/></returns>");
            sb.Append($"        public static Task<Result<{outputProperty}?>> {methodName} (");
            for (int i = 0; i < inputProperties.Count; i++)
            {
                sb.Append($"{inputProperties[i]}");
                if (i < inputProperties.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.AppendLine($")");
            sb.AppendLine("        {");

            switch (item.Definition.Type)
            {
                case "procedure":
                    sb.AppendLine($"            var endpointUrl = {item.ClassName}.ToString();");
                    sb.AppendLine($"            var headers = new Dictionary<string, string>();");
                    if (item.Id.Contains("moderation"))
                    {
                        sb.AppendLine($"            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);");
                    }
                    else if (item.Id.Contains("chat"))
                    {
                        sb.AppendLine($"            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);");
                    }
                    else if (item.Id.Contains("ozone"))
                    {
                        sb.AppendLine($"            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);");
                    }
                    if (inputProperties.Count <= 2)
                    {
                        sb.AppendLine(
                            $"            return atp.Post<{outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, cancellationToken, headers);");
                    }
                    else if (inputProperties[1].Contains("StreamContent"))
                    {
                        sb.AppendLine(
                            $"            return atp.Post<{outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, {inputProperties[1].Split(" ").Last()}, cancellationToken, headers);");
                    }
                    else
                    {
                        sb.AppendLine($"            var inputItem = new {item.ClassName}Input();");
                        var inputSourceContext =
                            $"{item.CSharpNamespace.Replace(".", string.Empty)}{item.ClassName}Input";
                        for (int i = 1; i < inputProperties.Count - 1; i++)
                        {
                            var prop = inputProperties[i].Split(" ")[1];
                            var realName = prop.Replace("@", string.Empty);
                            var property = item.Definition.Input?.Schema?.Properties.FirstOrDefault(n => n.Key == realName).Value;
                            if (property?.Type == "union")
                            {
                                if (property?.Refs?.Any() ?? false)
                                {
                                    // Switch statement to take test the {prop}.Type and check if its listed in any of the refs. If not, warn the user.
                                    sb.AppendLine($"            switch ({prop}.Type)");
                                    sb.AppendLine("            {");
                                    foreach (var unionType in property.Refs!)
                                    {
                                        sb.AppendLine($"                case \"{unionType}\":");
                                    }

                                    sb.AppendLine("                    break;");

                                    sb.AppendLine("                default:");
                                    var name =
                                    sb.AppendLine(
                                        $"                    atp.Options.Logger?.LogWarning($\"Unknown {prop} type for union: \" + {prop}.Type);");
                                    sb.AppendLine("                    break;");
                                    sb.AppendLine("            }");
                                }
                            }
                            sb.AppendLine(
                                $"            inputItem.{realName.ToPascalCase()} = {prop};");
                        }

                        sb.AppendLine(
                            $"            return atp.Post<{item.ClassName}Input, {outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{inputSourceContext}!, atp.Options.SourceGenerationContext.{sourceContext}!, inputItem, cancellationToken, headers);");
                    }

                    break;
                case "query":
                    sb.AppendLine($"            var endpointUrl = {item.ClassName}.ToString();");
                    if (inputProperties.Count > 2)
                    {
                        sb.AppendLine("            endpointUrl += \"?\";");
                        sb.AppendLine("            List<string> queryStrings = new();");
                    }

                    for (int i = 1; i < inputProperties.Count - 1; i++)
                    {
                        var typeName = inputProperties[i].Split(" ")[0];
                        var prop = inputProperties[i].Split(" ")[1];
                        var isList = typeName.Contains("List<");
                        if (isList)
                        {
                            if (typeName.Contains("?"))
                            {
                                sb.AppendLine($"            if ({prop} != null)");
                                sb.AppendLine("            {");
                                sb.AppendLine(
                                    $"                queryStrings.Add(string.Join(\"&\", {prop}.Select(n => \"{prop}=\" + n)));");
                                sb.AppendLine("            }");
                                sb.AppendLine();
                            }
                            else
                            {
                                sb.AppendLine(
                                    $"            queryStrings.Add(string.Join(\"&\", {prop}.Select(n => \"{prop}=\" + n)));");
                                sb.AppendLine();
                            }
                        }
                        else
                        {
                            if (typeName.Contains("?"))
                            {
                                sb.AppendLine($"            if ({prop} != null)");
                                sb.AppendLine("            {");
                                if (typeName.StartsWith("bool"))
                                {
                                    sb.AppendLine($"                queryStrings.Add(\"{prop}=\" + ({prop}.Value ? \"true\" : \"false\"));");
                                }
                                else
                                {
                                    sb.AppendLine($"                queryStrings.Add(\"{prop}=\" + {prop});");
                                }

                                sb.AppendLine("            }");
                                sb.AppendLine();
                            }
                            else
                            {
                                if (typeName.StartsWith("bool"))
                                {
                                    sb.AppendLine($"            queryStrings.Add(\"{prop}=\" + ({prop} ? \"true\" : \"false\"));");
                                }
                                else
                                {
                                    sb.AppendLine($"            queryStrings.Add(\"{prop}=\" + {prop});");
                                }
                                sb.AppendLine();
                            }
                        }
                    }

                    sb.AppendLine($"            var headers = new Dictionary<string, string>();");
                    if (item.Id.Contains("moderation"))
                    {
                        sb.AppendLine($"            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);");
                    }
                    else if (item.Id.Contains("chat"))
                    {
                        sb.AppendLine($"            headers.Add(Constants.AtProtoProxy, Constants.BlueskyChatProxy);");
                    }
                    else if (item.Id.Contains("ozone"))
                    {
                        sb.AppendLine($"            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);");
                    }

                    sb.AppendLine($"            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);");

                    if (inputProperties.Count > 2)
                    {
                        sb.AppendLine("            endpointUrl += string.Join(\"&\", queryStrings);");
                    }

                    if (inputProperties.Any(n => n.Contains("OnCarDecoded")))
                    {
                        sb.AppendLine(
                            $"            return atp.GetCarAsync(endpointUrl, cancellationToken, onDecoded);");
                    }
                    else if (outputProperty == "byte[]")
                    {
                        sb.AppendLine(
                            $"            return atp.GetBlob(endpointUrl, cancellationToken);");
                    }
                    else
                    {
                        sb.AppendLine(
                            $"            return atp.Get<{outputProperty}>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, cancellationToken, headers);");
                    }

                    break;
                default:
                    sb.AppendLine("            throw new NotImplementedException();");
                    break;
            }

            sb.AppendLine("        }");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, group.Key.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{className}.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: {className} {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private string PropertyNameToCSharpSafeValue(string propertyName)
    {
        // if the property name is a reserved keyword, prefix it with an @
        return new[]
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
            "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
            "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
            "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof",
            "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
        }.Contains(propertyName)
            ? $"@{propertyName}"
            : propertyName;
    }

    private (List<string> RequiredProperties, List<string> OptionalProperties) FetchInputPropertiesFromProperties(
        ClassGeneration classGeneration, bool isExtensionMethod = false, bool includeCancellationToken = true)
    {
        var requiredProperties = new List<string>();
        var optionalProperties = new List<string>();
        var requiredFields = classGeneration.Definition.Record?.Required?.ToList() ?? new List<string>();

        foreach (var prop in classGeneration.Properties)
        {
            var propertyName = this.PropertyNameToCSharpSafeValue(prop.Key);
            var isRequired = requiredFields.Contains(prop.Key);

            if (prop.Key == "createdAt")
            {
                isRequired = false;
                prop.PropertyDefinition.Default = "DateTime.UtcNow";
            }

            var returnType = prop.Type;

            propertyName += !isRequired ? " = default" : string.Empty;
            (isRequired ? requiredProperties : optionalProperties).Add($"{returnType} {propertyName}");
        }

        if (isExtensionMethod)
        {
            requiredProperties.Insert(0, "this FishyFlip.ATProtocol atp");
        }

        if (includeCancellationToken)
        {
            optionalProperties.Add("CancellationToken cancellationToken = default");
        }

        return (requiredProperties, optionalProperties);
    }

    private (List<string> RequiredProperties, List<string> OptionalProperties) FetchInputProperties(
    ClassGeneration classGeneration, bool isExtensionMethod = false)
    {
        var requiredProperties = new List<string>();
        var optionalProperties = new List<string>();

        foreach (var prop in classGeneration.Definition.Input?.Schema?.Properties ?? new Dictionary<string, PropertyDefinition>())
        {
            var propertyName = this.PropertyNameToCSharpSafeValue(prop.Key);
            var returnType = prop.Value.CSharpType;

            if (prop.Value.Ref is not null)
            {
                var refString = prop.Value.Ref.Split("#")[0] == string.Empty ? $"{classGeneration.Id}#{prop.Value.Ref.Split("#")[1]}" : prop.Value.Ref;
                var classRef = FindClassFromRef(refString);
                if (classRef is not null)
                {
                    returnType = this.GenerateReturnTypeFromClassGeneration(classRef);
                }
            }
            else if (prop.Value.Type == "array" && prop.Value.Items?.Ref is not null)
            {
                var refString = prop.Value.Items.Ref.Split("#")[0] == string.Empty ? $"{classGeneration.Id}#{prop.Value.Items.Ref.Split("#")[1]}" : prop.Value.Items.Ref;
                var classRef = FindClassFromRef(refString);
                if (classRef is not null)
                {
                    returnType = $"List<{this.GenerateReturnTypeFromClassGeneration(classRef)}>";
                }
            }

            if (!classGeneration.Definition.Input?.Schema?.Required.Contains(prop.Key) ?? false)
            {
                returnType += "?";
                propertyName += prop.Value.Type == "integer" ? $" = {prop.Value.Default ?? 0}" : " = default";
                optionalProperties.Add($"{returnType} {propertyName}");
            }
            else
            {
                requiredProperties.Add($"{returnType} {propertyName}");
            }
        }

        if (classGeneration.Definition.Parameters is not null)
        {
            foreach (var param in classGeneration.Definition.Parameters.Properties)
            {
                if (param.Value.Description.ToLowerInvariant().Contains("deprecated")) continue;

                var propertyName = this.PropertyNameToCSharpSafeValue(param.Key);
                var returnType = param.Value.CSharpType;

                if (!classGeneration.Definition.Parameters.Required.Contains(param.Key))
                {
                    returnType += "?";
                    propertyName += param.Value.Type == "integer" ? $" = {param.Value.Default ?? 0}" : " = default";
                    optionalProperties.Add($"{returnType} {propertyName}");
                }
                else
                {
                    requiredProperties.Add($"{returnType} {propertyName}");
                }
            }
        }

        if (classGeneration.Definition.Input?.Encoding == "*/*" || classGeneration.Definition.Input?.Encoding == "application/vnd.ipld.car")
        {
            requiredProperties.Add("StreamContent content");
        }

        if (classGeneration.Definition.Output?.Encoding == "application/vnd.ipld.car")
        {
            requiredProperties.Add("OnCarDecoded onDecoded");
        }

        if (isExtensionMethod)
        {
            requiredProperties.Insert(0, "this FishyFlip.ATProtocol atp");
        }

        optionalProperties.Add("CancellationToken cancellationToken = default");

        return (requiredProperties, optionalProperties);
    }

    private string FetchOutputProperties(ClassGeneration classGeneration)
    {
        if (classGeneration.Definition.Output?.Encoding == "*/*")
        {
            return "byte[]";
        }

        if (classGeneration.Definition.Output?.Schema is null)
        {
            return "Success";
        }

        if (classGeneration.Definition.Output.Schema.Type == "ref")
        {
            var refString = classGeneration.Definition.Output.Schema.Ref;
            var refSplit = classGeneration.Definition.Output.Schema.Ref.Split("#");
            if (string.IsNullOrEmpty(refSplit[0]))
            {
                refString = $"{classGeneration.Id}#{refSplit[1]}";
            }

            var classRef = FindClassFromRef(refString);
            if (classRef is not null)
            {
                return $"{this.GenerateReturnTypeFromClassGeneration(classRef)}";
            }
        }

        if (classGeneration.Definition.Output.Schema.Type == "object")
        {
            // Find the generated Output ref.
            var outputRef = $"{classGeneration.Id}#{classGeneration.ClassName}Output";
            var classRef = FindClassFromRef(outputRef);
            if (classRef is not null)
            {
                return $"{this.GenerateReturnTypeFromClassGeneration(classRef)}";
            }
        }

        Console.WriteLine($"Could not find output type for {classGeneration.Id}.");
        return "Success";
    }

    private string GenerateReturnTypeFromClassGeneration(ClassGeneration classRef)
    {
        if (classRef.IsArray)
        {
            if (classRef.IsArrayOfATObjects)
            {
                return $"List<ATObject>";
            }

            if (classRef.IsArrayOfStrings)
            {
                return $"List<string>";
            }
        }

        if (classRef.IsBaseType)
        {
            return classRef.Definition.Type switch
            {
                "string" => "string",
                "number" => "double",
                "boolean" => "bool",
                _ => "ATObject",
            };
        }

        return $"{baseNamespace}.{classRef.CSharpNamespace}.{classRef.ClassName}";
    }

    private List<string> GetMainRecordJsonFiles(List<string> jsonFiles)
    {
        var mainRecordFiles = new List<string>();
        foreach (var file in jsonFiles)
        {
            var jsonText = File.ReadAllText(file);
            var schemaDocument =
                JsonSerializer.Deserialize<SchemaDocument>(jsonText, SourceGenerationContext.Default.SchemaDocument);
            if (schemaDocument == null)
            {
                throw new Exception($"Failed to deserialize {file}.");
            }

            var mainRecord = schemaDocument.Defs.FirstOrDefault(n => n.Key == "main");
            if (mainRecord.Value != null)
            {
                if (mainRecord.Value.Type == "record")
                {
                    mainRecordFiles.Add(file);
                }
            }
        }

        return mainRecordFiles;
    }

    public static bool DoesRefStringHaveNoNamespace(string refString)
    {
        return refString.Contains("#") && !refString.Contains(".");
    }

    public static ClassGeneration? FindClassFromRef(string refString)
    {
        var record = AllClasses.FirstOrDefault(n => n.Id == refString);
        if (record is null)
        {
            refString = refString.Contains("#") ? refString.Split("#")[0] : refString;
            record = AllClasses.FirstOrDefault(n => n.Id == refString);
        }

        return record;
    }

    private async Task ProcessFile(string defJsonPath)
    {
        var defJsonText = await File.ReadAllTextAsync(defJsonPath);
        var schemaDocument =
            JsonSerializer.Deserialize<SchemaDocument>(defJsonText, SourceGenerationContext.Default.SchemaDocument);
        if (schemaDocument == null)
        {
            throw new Exception($"Failed to deserialize {defJsonPath}.");
        }

        if (AllClasses.Any(n => n.Id == schemaDocument.Id))
        {
            Console.WriteLine($"Skipping duplicate class: {schemaDocument.Id}");
            return;
        }

        var path = Path.Combine(schemaDocument.Id.Split('.').Take(schemaDocument.Id.Split('.').Length - 1)
            .Select(n => n.ToPascalCase()).ToArray());
        foreach (var definition in schemaDocument.Defs)
        {
            var classGeneration = new ClassGeneration(schemaDocument, definition.Key, definition.Value, path);
            Console.WriteLine(classGeneration.ToString());
            if (classGeneration.Definition.Description.Contains("deprecated", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Skipping deprecated class: {classGeneration.Id}");
                continue;
            }

            AllClasses.Add(classGeneration);
            if (classGeneration.Definition.Output is not null &&
                classGeneration.Definition.Output.Schema?.Type == "object")
            {
                var outputClass = new ClassGeneration(classGeneration.Document, $"{classGeneration.ClassName}Output",
                    classGeneration.Definition.Output.Schema, classGeneration.Path);
                AllClasses.Add(outputClass);
            }

            if (classGeneration.Definition.Input is not null)
            {
                var outputClass = new ClassGeneration(classGeneration.Document, $"{classGeneration.ClassName}Input",
                    classGeneration.Definition.Input.Schema, classGeneration.Path);
                AllClasses.Add(outputClass);
            }
        }
    }

    private async Task GenerateJsonSerializerContextFile(List<ClassGeneration> classes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, baseNamespace);
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// ATProtocol Message Source Generation Context.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    [JsonSourceGenerationOptions(");
        sb.AppendLine($"        WriteIndented = true,");
        sb.AppendLine($"        PropertyNameCaseInsensitive = true,");
        sb.AppendLine("        Converters = new [] {");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATUriJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATCidJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATHandleJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATDidJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATWebSocketCommitTypeConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATWebSocketEventConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATObjectJsonConverter),");
        sb.AppendLine("        },");
        sb.AppendLine($"        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,");
        sb.AppendLine(
            $"        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]");
        foreach (var cls in classes)
        {
            var typeName = $"{baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}";
            var typeInfoPropertyName = $"{cls.CSharpNamespace}.{cls.ClassName}".Replace(".", string.Empty);
            sb.AppendLine(
                $"    [JsonSerializable(typeof({typeName}), TypeInfoPropertyName = \"{typeInfoPropertyName}\")]");
            sb.AppendLine(
                $"    [JsonSerializable(typeof(List<{typeName}>), TypeInfoPropertyName = \"List{typeInfoPropertyName}\")]");
        }

        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketCommit))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketCommitType))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketEvent))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATWebSocketRecord))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATDid))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATError))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATHandle))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATIdentifier))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATLinkRef))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.ATUri))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.Blob))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.AuthSession))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.Session))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.Success))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.UnknownATObject))]");
        sb.AppendLine($"    [JsonSerializable(typeof(FishyFlip.Models.OAuthClientMetadata))]");
        sb.AppendLine($"    [JsonSerializable(typeof(OidcClientOptions))]");
        sb.AppendLine($"    [JsonSerializable(typeof(DPoPProofPayload))]");
        sb.AppendLine($"    [JsonSerializable(typeof(Dictionary<string, JsonElement>))]");
        sb.AppendLine(
            $"    [JsonSerializable(typeof(Microsoft.IdentityModel.Tokens.JsonWebKey), TypeInfoPropertyName = nameof(Microsoft.IdentityModel.Tokens.JsonWebKey) + \"_A\")]");
        sb.AppendLine($"    [JsonSerializable(typeof(List<ATObject>))]");
        sb.AppendLine($"    internal partial class SourceGenerationContext : JsonSerializerContext");
        sb.AppendLine("    {");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, "SourceGenerationContext.g.cs");
        await File.WriteAllTextAsync(outputPath, sb.ToString());
    }

    private async Task GenerateModelFile(ClassGeneration cls)
    {
        Console.WriteLine($"Generating Class: {cls.Id}, {cls.CSharpNamespace}, {cls.ClassName}, {cls.Definition.Type}");

        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{baseNamespace}.{cls.CSharpNamespace}");
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, cls.Definition);

        sb.AppendLine($"    public partial class {cls.ClassName} : ATObject");
        sb.AppendLine("    {");

        this.GenerateClassConstructor(sb, cls);
        this.GenerateEmptyClassConstructor(sb, cls);
        this.GenerateCBorObjectClassConstructor(sb, cls);
        foreach (var property in cls.Properties)
        {
            await this.GenerateProperty(sb, property, cls);
        }

        this.GenerateTypeProperty(sb, cls.Id);

        sb.AppendLine();

        this.GenerateFromJsonWithSourceGenerator(sb, cls);

        sb.AppendLine("    }");

        var nonAtObjectProperties = cls.Properties.Where(n => n.PropertyDefinition.Type == "object" && !AllClasses.Any(y => y.Key == n.Key)).ToList();
        foreach (var prop in nonAtObjectProperties)
        {
            sb.AppendLine();
            sb.AppendLine($"    public partial class {prop.ClassName}");
            sb.AppendLine("    {");
            foreach (var prop2 in prop.PropertyDefinition.Properties)
            {
                var newProp = new PropertyGeneration(prop2.Value, prop2.Key, cls);
                this.GenerateProperty(sb, newProp, cls);
            }
            // foreach (var prop2 in prop.PropertyDefinition.Properties!)
            // {
            //     sb.AppendLine($"        [JsonPropertyName(\"{prop2.Key}\")]");
            //     sb.AppendLine($"        public {prop2.Value.CSharpType} {prop2.Value.} {{ get; set; }}");
            // }

            sb.AppendLine("    }");
        }

        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, cls.Path);
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{cls.ClassName}.g.cs");
        if (File.Exists(classPath))
        {
            Console.WriteLine($"File already exists: {cls.Id} {classPath}");
            return;
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private void GenerateFromJsonWithSourceGenerator(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine($"        public static {cls.ClassName} FromJson(string json)");
        sb.AppendLine("        {");
        sb.AppendLine(
            $"            return JsonSerializer.Deserialize<{baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}>(json, (JsonTypeInfo<{baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
        sb.AppendLine("        }");
    }

    private void GenerateCBorObjectClassConstructor(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{cls.ClassName}\"/> class.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine($"        public {cls.ClassName}(CBORObject obj)");
        sb.AppendLine("        {");
        foreach (var property in cls.Properties)
        {
            sb.AppendLine($"            {property.CBorProperty}");
        }

        sb.AppendLine("        }");
        sb.AppendLine();
    }

    private void GenerateClassConstructor(StringBuilder sb, ClassGeneration cls)
    {
        var (requiredProperties, optionalProperties) = this.FetchInputPropertiesFromProperties(cls, false, false);
        var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
        if (inputProperties.Count == 0)
        {
            return;
        }

        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{cls.ClassName}\"/> class.");
        sb.AppendLine("        /// </summary>");
        foreach (var item in inputProperties)
        {
            sb.Append($"        /// <param name=\"{item.Split(" ")[1]}\">");
            var propName = item.Split(" ")[1].Replace("@", string.Empty);
            var prop = cls.Properties.FirstOrDefault(n => n.Key == propName);
            if (prop is null)
            {
                sb.AppendLine("</param>");
                continue;
            }

            if (!string.IsNullOrEmpty(prop?.PropertyDefinition.Description))
            {
                sb.Append(prop.PropertyDefinition.Description);
            }

            if (prop?.PropertyDefinition.Type == "ref")
            {
                sb.AppendLine();
                var refString = prop.PropertyDefinition.Ref;
                var refSplit = prop.PropertyDefinition.Ref!.Split("#");
                if (string.IsNullOrEmpty(refSplit[0]))
                {
                    refString = $"{cls.Namespace}.defs#{refSplit[1]}";
                }

                var cls2 = FindClassFromRef(refString);
                if (cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object")
                {
                    sb.AppendLine(
                        $"        /// <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
                }
                else
                {
                    if (cls2?.Definition.KnownValues?.Any() ?? false)
                    {
                        sb.AppendLine($"        /// <br/> Known Values: <br/>");
                        foreach (var knownValue in cls2.Definition.KnownValues)
                        {
                            var refString2 = knownValue;
                            var refSplit2 = knownValue.Split("#");
                            if (string.IsNullOrEmpty(refSplit2[0]))
                            {
                                refString2 = $"{cls2.Id}#{refSplit2[1]}";
                            }

                            var cls3 = FindClassFromRef(refString2);
                            if (cls3 is not null)
                            {
                                sb.AppendLine($"        /// {knownValue} - {cls3.Definition.Description} <br/>");
                            }
                            else
                            {
                                sb.AppendLine($"        /// {knownValue} <br/>");
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine($"        /// {refString} <br/>");
                    }
                }

                sb.AppendLine("        /// </param>");
            }
            else if (prop?.PropertyDefinition.Type == "union" || (prop?.PropertyDefinition.Type == "array" &&
                                                                  prop?.PropertyDefinition.Items?.Type == "union"))
            {
                sb.AppendLine();
                sb.AppendLine($"        /// <br/> Union Types: <br/>");
                var refs = prop.PropertyDefinition.Refs ?? prop.PropertyDefinition.Items?.Refs;
                foreach (var unionType in refs!)
                {
                    var refString = unionType;
                    var refSplit = unionType.Split("#");
                    if (string.IsNullOrEmpty(refSplit[0]))
                    {
                        refString = $"{cls.Namespace}.defs#{refSplit[1]}";
                    }

                    var cls2 = FindClassFromRef(refString);
                    if (cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object")
                    {
                        sb.AppendLine(
                            $"        /// <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString}) <br/>");
                    }
                    else
                    {
                        sb.AppendLine($"        /// {unionType} <br/>");
                    }
                }

                sb.AppendLine("        /// </param>");
            }
            else if (prop?.PropertyDefinition.KnownValues?.Any() ?? false)
            {
                sb.AppendLine();
                sb.AppendLine($"        /// <br/> Known Values: <br/>");
                foreach (var knownValue in prop.PropertyDefinition.KnownValues)
                {
                    var refString = knownValue;
                    var refSplit = knownValue.Split("#");
                    if (string.IsNullOrEmpty(refSplit[0]))
                    {
                        refString = $"{cls.Id}#{refSplit[1]}";
                    }

                    var cls2 = FindClassFromRef(refString);
                    if (cls2 is not null)
                    {
                        sb.AppendLine($"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description} <br/>");
                    }
                    else
                    {
                        sb.AppendLine($"        /// {knownValue.Split("#").Last()} <br/>");
                    }
                }

                sb.AppendLine("        /// </param>");
            }
            else
            {
                sb.AppendLine("</param>");
            }
        }

        sb.Append($"        public {cls.ClassName}(");
        for (int i = 0; i < inputProperties.Count; i++)
        {
            sb.Append($"{inputProperties[i]}");
            if (i < inputProperties.Count - 1)
            {
                sb.Append(", ");
            }
        }

        sb.AppendLine(")");
        sb.AppendLine("        {");
        foreach (var property in cls.Properties)
        {
            if (property.Key == "createdAt")
            {
                sb.AppendLine(
                    $"            this.{property.PropertyName} = {PropertyNameToCSharpSafeValue(property.Key)} ?? DateTime.UtcNow;");
            }
            else
            {
                sb.AppendLine(
                    $"            this.{property.PropertyName} = {PropertyNameToCSharpSafeValue(property.Key)};");
            }
        }

        sb.AppendLine($"            this.Type = \"{cls.Id}\";");
        sb.AppendLine("        }");
        sb.AppendLine();
    }

    private void GenerateEmptyClassConstructor(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{cls.ClassName}\"/> class.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine($"        public {cls.ClassName}()");
        sb.AppendLine("        {");
        sb.AppendLine($"            this.Type = \"{cls.Id}\";");
        sb.AppendLine("        }");
        sb.AppendLine();
    }

    private async Task GenerateProperty(StringBuilder sb, PropertyGeneration property, ClassGeneration cls)
    {
        Console.WriteLine($"Generating Property for {property.Document.Id}: {property.Key}, {property.Type}");

        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets or sets the {property.Key}.");
        // Add property documentation if available
        if (!string.IsNullOrEmpty(property.PropertyDefinition.Description))
        {
            sb.AppendLine($"        /// <br/> {property.PropertyDefinition.Description}");
        }

        if (property?.PropertyDefinition.Type == "ref")
        {
            var refString = property.PropertyDefinition.Ref;
            var refSplit = property.PropertyDefinition.Ref.Split("#");
            if (string.IsNullOrEmpty(refSplit[0]))
            {
                refString = $"{cls.Namespace}.defs#{refSplit[1]}";
            }

            var cls2 = FindClassFromRef(refString);
            if (cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object")
            {
                sb.AppendLine($"        /// <br/> <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
            }
            else
            {
                if (cls2?.Definition.KnownValues?.Any() ?? false)
                {
                    sb.AppendLine($"        /// <br/> Known Values: <br/>");
                    foreach (var knownValue in cls2.Definition.KnownValues)
                    {
                        var refString2 = knownValue;
                        var refSplit2 = knownValue.Split("#");
                        if (string.IsNullOrEmpty(refSplit2[0]))
                        {
                            refString2 = $"{cls2.Id}#{refSplit2[1]}";
                        }

                        var cls3 = FindClassFromRef(refString2);
                        if (cls3 is not null)
                        {
                            sb.AppendLine($"        /// {knownValue} - {cls3.Definition.Description} <br/>");
                        }
                        else
                        {
                            sb.AppendLine($"        /// {knownValue} <br/>");
                        }
                    }
                }
                else
                {
                    sb.AppendLine($"        /// {refString} <br/>");
                }
            }
        }

        if (property?.PropertyDefinition.Type == "union" || (property?.PropertyDefinition.Type == "array" &&
                                                             property?.PropertyDefinition.Items?.Type == "union"))
        {
            sb.AppendLine($"        /// <br/> Union Types: <br/>");
            var refs = property.PropertyDefinition.Refs ?? property.PropertyDefinition.Items?.Refs;
            foreach (var unionType in refs!)
            {
                var refString = unionType;
                var refSplit = unionType.Split("#");
                if (string.IsNullOrEmpty(refSplit[0]))
                {
                    refString = $"{cls.Id.Split("#").First()}#{refSplit[1]}";
                }

                var cls2 = FindClassFromRef(refString);
                if (cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object")
                {
                    sb.AppendLine(
                        $"        /// <see cref=\"{baseNamespace}.{cls2.FullClassName}\"/> ({refString}) <br/>");
                }
                else
                {
                    sb.AppendLine($"        /// {unionType} <br/>");
                }
            }
        }
        else if (property?.PropertyDefinition.KnownValues?.Any() ?? false)
        {
            sb.AppendLine($"        /// <br/> Known Values: <br/>");
            foreach (var knownValue in property.PropertyDefinition.KnownValues)
            {
                var refString = knownValue;
                var refSplit = knownValue.Split("#");
                if (string.IsNullOrEmpty(refSplit[0]))
                {
                    refString = $"{cls.Id}#{refSplit[1]}";
                }

                var cls2 = FindClassFromRef(refString);
                if (cls2 is not null)
                {
                    sb.AppendLine($"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description} <br/>");
                }
                else
                {
                    sb.AppendLine($"        /// {knownValue.Split("#").Last()} <br/>");
                }
            }
        }

        sb.AppendLine($"        /// </summary>");

        // Add JSON property name attribute
        sb.AppendLine($"        [JsonPropertyName(\"{property.Key}\")]");

        // Add Required attribute if property is required
        if (property.Definition.Required?.Contains(property.Key) == true)
        {
            sb.AppendLine("        [JsonRequired]");
        }

        if (property.PropertyDefinition.Type == "array" && !property.IsBaseType)
        {
            var nonNullableRawType = property.RawType.Replace("?", null);
            if (nonNullableRawType == "FishyFlip.Models.ATUri")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATUriJsonConverter>))]");
            }
            else if (nonNullableRawType == "Ipfs.Cid")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATCidJsonConverter>))]");
            }
            else if (nonNullableRawType == "FishyFlip.Models.ATHandle")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATHandleJsonConverter>))]");
            }
            else if (nonNullableRawType == "FishyFlip.Models.ATDid")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATDidJsonConverter>))]");
            }
            else if (nonNullableRawType == "FishyFlip.Models.ATIdentifier")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATIdentifierJsonConverter>))]");
            }
        }

        var nonNullablePropertyType = property.Type.Replace("?", null);
        // Add Converter for various AT Object types (Ex. ATDid, ATUri, etc.)
        if (nonNullablePropertyType == "FishyFlip.Models.ATUri")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]");
        }

        if (nonNullablePropertyType == "Ipfs.Cid")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATCidJsonConverter))]");
        }

        if (nonNullablePropertyType == "FishyFlip.Models.ATHandle")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]");
        }

        if (nonNullablePropertyType == "FishyFlip.Models.ATDid")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]");
        }

        if (nonNullablePropertyType == "FishyFlip.Models.ATIdentifier")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATIdentifierJsonConverter))]");
        }

        // Handle default values
        if (property.PropertyDefinition.Default != null)
        {
            sb.AppendLine(
                $"        public {property.Type} {property.PropertyName} {{ get; set; }} = {property.GetDefaultValue()};");
        }
        else
        {
            sb.AppendLine($"        public {property.Type} {property.PropertyName} {{ get; set; }}");
        }

        sb.AppendLine();
    }

    private async Task GenerateCBORToATObjectConverterClassFile(List<ClassGeneration> classes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, "FishyFlip.Lexicon");
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Converts CBOR to ATObjects.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class CborLexiconExtensions");
        sb.AppendLine("    {");
        sb.AppendLine();
        sb.AppendLine("        public static bool IsATObject(this CBORObject obj)");
        sb.AppendLine("        {");
        sb.AppendLine("            return obj?.ContainsKey(\"$type\") ?? false;");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        public static ATObject ToATObject(this CBORObject obj, IReadOnlyList<ICustomATObjectCBORConverter> converters = null)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (obj == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                 throw new NullReferenceException(nameof(obj));");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine("            var type = obj[\"$type\"]?.AsString() ?? string.Empty;");
        sb.AppendLine("            switch (type)");
        sb.AppendLine("            {");
        foreach (var cls in classes)
        {
            sb.AppendLine($"                case \"{cls.Id}\":");
            sb.AppendLine($"                    return new {cls.CSharpNamespace}.{cls.ClassName}(obj);");
        }

        sb.AppendLine("                default:");
        sb.AppendLine("                    if (converters != null)");
        sb.AppendLine("                    {");
        sb.AppendLine("                        foreach (var converter in converters)");
        sb.AppendLine("                        {");
        sb.AppendLine("                            if (converter.SupportedTypes.Contains(type))");
        sb.AppendLine("                            {");
        sb.AppendLine("                                return converter.Read(obj, type);");
        sb.AppendLine("                            }");
        sb.AppendLine("                        }");
        sb.AppendLine("                    }");
        sb.AppendLine();
        sb.AppendLine("                    return new FishyFlip.Models.UnknownATObject(obj);");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        public static List<ATObject>? ToATObjectList(this CBORObject obj)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (obj == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                return null;");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine("            var list = new List<ATObject>();");
        sb.AppendLine("            foreach (var item in obj.Values)");
        sb.AppendLine("            {");
        sb.AppendLine("                list.Add(item.ToATObject());");
        sb.AppendLine("            }");
        sb.AppendLine("            return list;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, "CborExtensions.g.cs");
        await File.WriteAllTextAsync(outputPath, sb.ToString());
    }

    private string GenerateATObjectSource(string ns, List<ClassGeneration> classes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, ns);
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// The base class for FishyFlip ATProtocol Objects.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public class ATObject");
        sb.AppendLine("    {");
        sb.AppendLine();
        sb.AppendLine("        public ATObject() { }");
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets or sets the ATRecord Type.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        [JsonPropertyName(\"$type\")]");
        sb.AppendLine($"        [JsonPropertyOrder(-1)]");
        sb.AppendLine($"        public string Type {{ get; set; }} = string.Empty;");
        sb.AppendLine();
        sb.AppendLine($"        public virtual string ToJson()");
        sb.AppendLine("        {");
        sb.AppendLine("            switch (this.Type)");
        sb.AppendLine("            {");
        foreach (var cls in classes)
        {
            sb.AppendLine($"                case \"{cls.Id}\":");
            sb.AppendLine($"                    return JsonSerializer.Serialize(({AppCommands.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName})this, SourceGenerationContext.Default.{cls.CSharpNamespace.Replace(".", string.Empty)}{cls.ClassName});");
        }

        sb.AppendLine($"                case \"blob\":");
        sb.AppendLine($"                    return JsonSerializer.Serialize((FishyFlip.Models.Blob)this, SourceGenerationContext.Default.Blob);");
        sb.AppendLine("                default:");
        sb.AppendLine("                    return JsonSerializer.Serialize(this, SourceGenerationContext.Default.ATObject);");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine($"        internal byte[]? ToUtf8Json()");
        sb.AppendLine("        {");
        sb.AppendLine("            switch (this.Type)");
        sb.AppendLine("            {");
                foreach (var cls in classes)
        {
            sb.AppendLine($"                case \"{cls.Id}\":");
            sb.AppendLine($"                    return JsonSerializer.SerializeToUtf8Bytes(({AppCommands.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName})this, SourceGenerationContext.Default.{cls.CSharpNamespace.Replace(".", string.Empty)}{cls.ClassName});");
        }

        sb.AppendLine($"                case \"blob\":");
        sb.AppendLine($"                    return JsonSerializer.SerializeToUtf8Bytes((FishyFlip.Models.Blob)this, SourceGenerationContext.Default.Blob);");
        sb.AppendLine("                default:");
        sb.AppendLine("                    return null;");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("        internal static ATObject? ToATObject(string text, string type)");
        sb.AppendLine("        {");
        sb.AppendLine("            switch (type)");
        sb.AppendLine("            {");
        foreach (var cls in classes)
        {
            sb.AppendLine($"                case \"{cls.Id}\":");
            sb.AppendLine($"                    return JsonSerializer.Deserialize<{AppCommands.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}>(text, (JsonTypeInfo<{AppCommands.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{cls.CSharpNamespace.Replace(".", string.Empty)}{cls.ClassName});");
        }

        sb.AppendLine($"                case \"blob\":");
        sb.AppendLine($"                    return JsonSerializer.Deserialize<FishyFlip.Models.Blob>(text, (JsonTypeInfo<FishyFlip.Models.Blob>)SourceGenerationContext.Default.Blob);");
        sb.AppendLine("                default:");
        sb.AppendLine("                    return null;");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
    }

    private void GenerateTypeProperty(StringBuilder sb, string id)
    {
        sb.AppendLine($"        public const string RecordType = \"{id}\";");
    }

    private void GenerateHeader(StringBuilder sb)
    {
        sb.AppendLine("// <auto-generated />");
        sb.AppendLine("// This file was generated by FFSourceGen.");
        sb.AppendLine("// Do not modify this file.");
        sb.AppendLine();

        // Add #nullable
        sb.AppendLine("#nullable enable annotations");
        sb.AppendLine("#nullable disable warnings");
        sb.AppendLine();
    }

    private void GenerateNamespace(StringBuilder sb, string ns)
    {
        sb.AppendLine($"namespace {ns}");
    }

    private void GenerateClassDocumentation(StringBuilder sb, SchemaDefinition definition)
    {
        // Add class documentation if available
        if (!string.IsNullOrEmpty(definition.Description))
        {
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {definition.Description}");
            sb.AppendLine($"    /// </summary>");
        }
    }

    private async Task GenerateATErrorClasses(List<string> errorCodes)
    {
        var errorPath = Path.Combine(this.basePath, "Errors");
        Directory.CreateDirectory(errorPath);
        foreach (var errorCode in errorCodes)
        {
            var sb = new StringBuilder();
            this.GenerateHeader(sb);
            this.GenerateNamespace(sb, baseNamespace);
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// FishyFlip ATError for {errorCode}.");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public class {errorCode.ToPascalCase()}Error : FishyFlip.Models.ATError");
            sb.AppendLine("    {");
            sb.AppendLine($"        public {errorCode.ToPascalCase()}Error(int statusCode, ErrorDetail detail) : base(statusCode, detail)");
            sb.AppendLine("        {");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine();
            var outputPath = Path.Combine(errorPath, $"{errorCode.ToPascalCase()}Error.g.cs");
            await File.WriteAllTextAsync(outputPath, sb.ToString());
        }
    }

    private async Task GenerateATErrorObjects(List<string> errorCodes)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, baseNamespace);
        sb.AppendLine("{");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Generates casted ATError messages if available.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public static class ATErrorGenerator");
        sb.AppendLine("    {");
        sb.AppendLine("        public static FishyFlip.Models.ATError Generate(int statusCode, ErrorDetail detail)");
        sb.AppendLine("        {");
        sb.AppendLine("            switch (detail.Error)");
        sb.AppendLine("            {");
        foreach (var errorCode in errorCodes)
        {
            sb.AppendLine($"                case \"{errorCode}\":");
            sb.AppendLine($"                    return new {errorCode.ToPascalCase()}Error(statusCode, detail);");
        }

        sb.AppendLine("                default:");
        sb.AppendLine("                    return new FishyFlip.Models.ATError(statusCode, detail);");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, "ATErrorGenerator.g.cs");
        await File.WriteAllTextAsync(outputPath, sb.ToString());
    }
}