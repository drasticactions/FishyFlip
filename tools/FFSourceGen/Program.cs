﻿// <copyright file="Program.cs" company="Drastic Actions">
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
    private string baseNamespace { get; set; } = "FishyFlip.Lexicon";
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
        this.baseNamespace = baseNamespace ??= this.baseNamespace;
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
        var files = Directory.EnumerateFiles(lexiconPath, "*.json", SearchOption.AllDirectories).ToList();
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

        await this.GenerateEndpointClassListForATProtoCS(procedureClasses);

        await this.GenerateJsonSerializerContextFile(modelClasses);
        await this.GenerateCBORToATObjectConverterClassFile(modelClasses);

        var atRecordSource = this.GenerateATObjectSource(this.baseNamespace, modelClasses);
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
    }

    private async Task GenerateConstantKnownValueClass(ClassGeneration cls)
    {
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{cls.CSharpNamespace}");
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
                $"        public {this.baseNamespace}.{group.Key}.{className} {className.Replace("ATProto", string.Empty).Replace("Bluesky", string.Empty)} => new (this);");
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
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{group.Key}");
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
        this.GenerateNamespace(sb, this.baseNamespace);
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
            inputProperties[inputProperties.IndexOf("ATObject record")] =
                $"{this.baseNamespace}.{item.FullClassName} record";
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
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
            sb.AppendLine($"            var record = new {this.baseNamespace}.{item.FullClassName}();");
            for (int i = 0; i < item.Properties.Count(); i++)
            {
                var prop = item.Properties[i].ClassName;
                var key = item.Properties[i].Key;
                if (key == "createdAt")
                {
                    sb.AppendLine($"            record.{prop} = {key} ?? DateTime.UtcNow;");
                }
                else
                {
                    sb.AppendLine($"            record.{prop} = {key};");
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
            inputProperties[inputProperties.IndexOf("ATObject record")] =
                $"{this.baseNamespace}.{item.FullClassName} record";
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
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
            inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
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
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{group.Key}");
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
            inputProperties[inputProperties.IndexOf("ATObject record")] = $"{item.FullClassName} record";
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
            sb.AppendLine($"            var record = new {this.baseNamespace}.{item.FullClassName}();");
            for (int i = 0; i < item.Properties.Count(); i++)
            {
                var prop = item.Properties[i].ClassName;
                var key = item.Properties[i].Key;
                if (key == "createdAt")
                {
                    sb.AppendLine($"            record.{prop} = {key} ?? DateTime.UtcNow;");
                }
                else
                {
                    sb.AppendLine($"            record.{prop} = {key};");
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
            inputProperties[inputProperties.IndexOf("ATObject record")] = $"{item.FullClassName} record";
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

    private void GenerateInputProperties(StringBuilder sb, List<string> inputProperties, bool removeStatic = false)
    {
        var start = removeStatic ? 1 : 0;
        for (int i = start; i < inputProperties.Count; i++)
        {
            sb.Append($"{inputProperties[i]}");
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
                            sb.AppendLine($"        /// Union Types:");
                            var refs = cls.Definition.Refs ?? cls.Definition.Items?.Refs;
                            foreach (var unionType in refs!)
                            {
                                var refString = unionType.Contains("#")
                                    ? $"{item.Namespace}.defs#{unionType.Split("#")[1]}"
                                    : unionType;
                                var cls2 = FindClassFromRef(refString);
                                sb.AppendLine(cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object"
                                    ? $"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})"
                                    : $"        /// {unionType}");
                            }

                            sb.AppendLine("        /// </param>");
                        }
                        else if (cls.Definition.KnownValues?.Any() ?? false)
                        {
                            sb.AppendLine();
                            sb.AppendLine($"        /// Known Values:");
                            foreach (var knownValue in cls.Definition.KnownValues)
                            {
                                var refString = knownValue.Contains("#")
                                    ? $"{item.Id}#{knownValue.Split("#")[1]}"
                                    : knownValue;
                                var cls2 = FindClassFromRef(refString);
                                sb.AppendLine(cls2 is not null
                                    ? $"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description}"
                                    : $"        /// {knownValue.Split("#").Last()}");
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
                        $"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
                }
                else if (cls2?.Definition.KnownValues?.Any() ?? false)
                {
                    sb.AppendLine($"        /// Known Values:");
                    foreach (var knownValue in cls2.Definition.KnownValues)
                    {
                        var refString2 = knownValue.Contains("#")
                            ? $"{cls2.Id}#{knownValue.Split("#")[1]}"
                            : knownValue;
                        var cls3 = FindClassFromRef(refString2);
                        sb.AppendLine(cls3 is not null
                            ? $"        /// {knownValue} - {cls3.Definition.Description}"
                            : $"        /// {knownValue}");
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
                sb.AppendLine($"        /// Union Types:");
                var refs = prop.PropertyDefinition.Refs ?? prop.PropertyDefinition.Items?.Refs;
                foreach (var unionType in refs!)
                {
                    var refString = unionType.Contains("#")
                        ? $"{item.Namespace}.defs#{unionType.Split("#")[1]}"
                        : unionType;
                    var cls2 = FindClassFromRef(refString);
                    sb.AppendLine(cls2?.Definition.Type == "record" || cls2?.Definition.Type == "object"
                        ? $"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})"
                        : $"        /// {unionType}");
                }

                sb.AppendLine("        /// </param>");
            }
            else if (prop.PropertyDefinition.KnownValues?.Any() ?? false)
            {
                sb.AppendLine();
                sb.AppendLine($"        /// Known Values:");
                foreach (var knownValue in prop.PropertyDefinition.KnownValues)
                {
                    var refString = knownValue.Contains("#") ? $"{item.Id}#{knownValue.Split("#")[1]}" : knownValue;
                    var cls2 = FindClassFromRef(refString);
                    sb.AppendLine(cls2 is not null
                        ? $"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description}"
                        : $"        /// {knownValue.Split("#").Last()}");
                }

                sb.AppendLine("        /// </param>");
            }
            else
            {
                sb.AppendLine("</param>");
            }
        }
    }

    private async Task GenerateEndpointGroupAsync(IGrouping<string, ClassGeneration> group)
    {
        Console.WriteLine($"Generating Endpoint Group: {group.Key}");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{group.Key}");
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
                    if (inputProperties.Count <= 2)
                    {
                        sb.AppendLine(
                            $"            return atp.Client.Post<{outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);");
                    }
                    else if (inputProperties[1].Contains("StreamContent"))
                    {
                        sb.AppendLine(
                            $"            return atp.Client.Post<{outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, {inputProperties[1].Split(" ").Last()}, cancellationToken, atp.Options.Logger);");
                    }
                    else
                    {
                        sb.AppendLine($"            var inputItem = new {item.ClassName}Input();");
                        var inputSourceContext =
                            $"{item.CSharpNamespace.Replace(".", string.Empty)}{item.ClassName}Input";
                        for (int i = 1; i < inputProperties.Count - 1; i++)
                        {
                            var prop = inputProperties[i].Split(" ")[1];
                            sb.AppendLine(
                                $"            inputItem.{prop.Replace("@", string.Empty).ToPascalCase()} = {prop};");
                        }

                        // return atp.Client.Post<FishyFlip.Lexicon.Com.Atproto.Server.CreateAccountInput?, FishyFlip.Lexicon.Com.Atproto.Server.CreateAccountOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoServerCreateAccountInput!, atp.Options.SourceGenerationContext.ComAtprotoServerCreateAccountOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
                        sb.AppendLine(
                            $"            return atp.Client.Post<{item.ClassName}Input, {outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{inputSourceContext}!, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);");
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
                                sb.AppendLine($"                queryStrings.Add(\"{prop}=\" + {prop});");
                                sb.AppendLine("            }");
                                sb.AppendLine();
                            }
                            else
                            {
                                sb.AppendLine($"            queryStrings.Add(\"{prop}=\" + {prop});");
                                sb.AppendLine();
                            }
                        }
                    }

                    if (inputProperties.Count > 2)
                    {
                        sb.AppendLine("            endpointUrl += string.Join(\"&\", queryStrings);");
                    }

                    if (inputProperties.Any(n => n.Contains("OnCarDecoded")))
                    {
                        sb.AppendLine(
                            $"            return atp.Client.GetCarAsync(endpointUrl, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger, onDecoded);");
                    }
                    else
                    {
                        sb.AppendLine(
                            $"            return atp.Client.Get<{outputProperty}>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);");
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

        return $"{this.baseNamespace}.{classRef.CSharpNamespace}.{classRef.ClassName}";
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
        return AllClasses.FirstOrDefault(n => n.Id == refString);
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
        this.GenerateNamespace(sb, this.baseNamespace);
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
        sb.AppendLine("        },");
        sb.AppendLine($"        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,");
        sb.AppendLine(
            $"        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]");
        foreach (var cls in classes)
        {
            var typeName = $"{this.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}";
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
        this.GenerateNamespace(sb, $"{this.baseNamespace}.{cls.CSharpNamespace}");
        sb.AppendLine("{");

        this.GenerateClassDocumentation(sb, cls.Definition);

        sb.AppendLine($"    public partial class {cls.ClassName} : ATObject");
        sb.AppendLine("    {");

        this.GenerateClassConstructor(sb, cls);
        this.GenerateEmptyClassConstructor(sb, cls.ClassName);
        this.GenerateCBorObjectClassConstructor(sb, cls);
        foreach (var property in cls.Properties)
        {
            await this.GenerateProperty(sb, property, cls);
        }

        this.GenerateTypeProperty(sb, cls.Id);

        sb.AppendLine();
        this.GenerateToJsonWithSourceGenerator(sb, cls);

        sb.AppendLine();
        this.GenerateFromJsonWithSourceGenerator(sb, cls);

        sb.AppendLine("    }");
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

    private void GenerateToJsonWithSourceGenerator(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine($"        public override string ToJson()");
        sb.AppendLine("        {");
        sb.AppendLine(
            $"            return JsonSerializer.Serialize<{cls.CSharpNamespace}.{cls.ClassName}>(this, (JsonTypeInfo<{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
        sb.AppendLine("        }");
    }

    private void GenerateFromJsonWithSourceGenerator(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine($"        public static {cls.ClassName} FromJson(string json)");
        sb.AppendLine("        {");
        sb.AppendLine(
            $"            return JsonSerializer.Deserialize<{cls.CSharpNamespace}.{cls.ClassName}>(json, (JsonTypeInfo<{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
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
                        $"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
                }
                else
                {
                    if (cls2?.Definition.KnownValues?.Any() ?? false)
                    {
                        sb.AppendLine($"        /// Known Values:");
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
                                sb.AppendLine($"        /// {knownValue} - {cls3.Definition.Description}");
                            }
                            else
                            {
                                sb.AppendLine($"        /// {knownValue}");
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine($"        /// {refString}");
                    }
                }

                sb.AppendLine("        /// </param>");
            }
            else if (prop?.PropertyDefinition.Type == "union" || (prop?.PropertyDefinition.Type == "array" &&
                                                                  prop?.PropertyDefinition.Items?.Type == "union"))
            {
                sb.AppendLine();
                sb.AppendLine($"        /// Union Types:");
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
                            $"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
                    }
                    else
                    {
                        sb.AppendLine($"        /// {unionType}");
                    }
                }

                sb.AppendLine("        /// </param>");
            }
            else if (prop?.PropertyDefinition.KnownValues?.Any() ?? false)
            {
                sb.AppendLine();
                sb.AppendLine($"        /// Known Values:");
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
                        sb.AppendLine($"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description}");
                    }
                    else
                    {
                        sb.AppendLine($"        /// {knownValue.Split("#").Last()}");
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

        sb.AppendLine("        }");
        sb.AppendLine();
    }

    private void GenerateEmptyClassConstructor(StringBuilder sb, string className)
    {
        sb.AppendLine();
        sb.AppendLine("        /// <summary>");
        sb.AppendLine($"        /// Initializes a new instance of the <see cref=\"{className}\"/> class.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine($"        public {className}()");
        sb.AppendLine("        {");
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
            sb.AppendLine($"        /// {property.PropertyDefinition.Description}");
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
                sb.AppendLine($"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
            }
            else
            {
                if (cls2?.Definition.KnownValues?.Any() ?? false)
                {
                    sb.AppendLine($"        /// Known Values:");
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
                            sb.AppendLine($"        /// {knownValue} - {cls3.Definition.Description}");
                        }
                        else
                        {
                            sb.AppendLine($"        /// {knownValue}");
                        }
                    }
                }
                else
                {
                    sb.AppendLine($"        /// {refString}");
                }
            }
        }

        if (property?.PropertyDefinition.Type == "union" || (property?.PropertyDefinition.Type == "array" &&
                                                             property?.PropertyDefinition.Items?.Type == "union"))
        {
            sb.AppendLine($"        /// Union Types:");
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
                        $"        /// <see cref=\"{this.baseNamespace}.{cls2.FullClassName}\"/> ({refString})");
                }
                else
                {
                    sb.AppendLine($"        /// {unionType}");
                }
            }
        }
        else if (property?.PropertyDefinition.KnownValues?.Any() ?? false)
        {
            sb.AppendLine($"        /// Known Values:");
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
                    sb.AppendLine($"        /// {knownValue.Split("#").Last()} - {cls2.Definition.Description}");
                }
                else
                {
                    sb.AppendLine($"        /// {knownValue.Split("#").Last()}");
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
            if (property.RawType == "FishyFlip.Models.ATUri?")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATUriJsonConverter>))]");
            }
            else if (property.RawType == "Ipfs.Cid?")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATCidJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATHandle?")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATHandleJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATDid?")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATDidJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATIdentifier?")
            {
                sb.AppendLine(
                    $"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATIdentifierJsonConverter>))]");
            }
        }

        // Add Converter for various AT Object types (Ex. ATDid, ATUri, etc.)
        if (property.Type == "FishyFlip.Models.ATUri?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATUriJsonConverter))]");
        }

        if (property.Type == "Ipfs.Cid?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATCidJsonConverter))]");
        }

        if (property.Type == "FishyFlip.Models.ATHandle?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATHandleJsonConverter))]");
        }

        if (property.Type == "FishyFlip.Models.ATDid?")
        {
            sb.AppendLine("        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]");
        }

        if (property.RawType == "FishyFlip.Models.ATIdentifier")
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
        sb.AppendLine("        public static ATObject ToATObject(this CBORObject obj)");
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
        foreach (var cls in classes)
        {
            sb.AppendLine(
                $"    [JsonDerivedType(typeof({cls.CSharpNamespace}.{cls.ClassName}), typeDiscriminator: \"{cls.Id}\")]");
        }

        sb.AppendLine($"    [JsonDerivedType(typeof(FishyFlip.Models.Blob), typeDiscriminator: \"blob\")]");
        sb.AppendLine($"    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true)]");
        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// The base class for FishyFlip ATProtocol Objects.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public class ATObject");
        sb.AppendLine("    {");
        sb.AppendLine();
        sb.AppendLine("        public ATObject() { }");
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets the ATRecord Type.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        [JsonPropertyName(\"$type\")]");
        sb.AppendLine($"        public virtual string Type {{ get; }} = string.Empty;");
        sb.AppendLine();
        sb.AppendLine($"        public virtual string ToJson()");
        sb.AppendLine("        {");
        sb.AppendLine(
            $"            return JsonSerializer.Serialize<ATObject>(this, (JsonTypeInfo<ATObject>)SourceGenerationContext.Default.ATObject)!;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        return sb.ToString();
    }

    private void GenerateTypeProperty(StringBuilder sb, string id)
    {
        sb.AppendLine($"        /// <summary>");
        sb.AppendLine($"        /// Gets the ATRecord Type.");
        sb.AppendLine($"        /// </summary>");
        sb.AppendLine($"        [JsonPropertyName(\"$type\")]");
        sb.AppendLine($"        public override string Type => \"{id}\";");
        sb.AppendLine();
        sb.AppendLine($"        public const string RecordType = \"{id}\";");
    }

    private void GenerateHeader(StringBuilder sb)
    {
        sb.AppendLine("// <auto-generated />");
        sb.AppendLine("// This file was generated by FFSourceGen.");
        sb.AppendLine("// Do not modify this file.");
        sb.AppendLine();

        // Add #nullable
        sb.AppendLine("#nullable enable");
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
}