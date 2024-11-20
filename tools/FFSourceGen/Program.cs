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

    /// <summary>
    /// Generate lexicon source code.
    /// </summary>
    /// <param name="lexiconPath">Path to lexicon files.</param>
    /// <param name="outputDir">-o, Output directory.</param>
    /// <param name="baseNamespace">-n, Base Namespace.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    [Command("generate")]
    public async Task GenerateAsync([Argument] string lexiconPath, string? outputDir = default, string? baseNamespace = default, CancellationToken cancellationToken = default)
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

        await this.GenerateModelFiles(lexiconPath);
    }

    private async Task GenerateModelFiles(string lexiconPath)
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

        foreach (var cls in AllClasses)
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

        var records = AllClasses.Where(n => n.Definition.Type == "record").GroupBy(n => n.CSharpNamespace).ToList();
        foreach (var record in records)
        {
            await this.GenerateRecordStaticExtensionClass(record);
            await this.GenerateRecordExtensionClass(record);
        }

        // var recordList = new string[] { "com.atproto.repo.createRecord", "com.atproto.repo.deleteRecord", "com.atproto.repo.putRecord", "com.atproto.repo.listRecords" };
        // foreach (var item in records)
        // {
        // }

        var modelClasses = AllClasses.Where(n => n.Definition.Type == "object" || n.Definition.Type == "record").ToList();
        var procedureClasses = AllClasses.Where(n => n.Definition.Type == "procedure" || n.Definition.Type == "query").GroupBy(n => n.CSharpNamespace).ToList();
        foreach (var group in procedureClasses)
        {
            await this.GenerateEndpointGroupAsync(group);
            await this.GenerateEndpointClassAsync(group);
        }

        await this.GenerateEndpointClassListForATProtoCS(procedureClasses);

        await this.GenerateJsonSerializerContextFile(modelClasses);
        await this.GenerateCBORToATObjectConverterClassFile(modelClasses);
        // await this.GenerateATProtocolEndpointListAsync(procedureClasses.Select(n => n.Key).ToList());

        var atRecordSource = this.GenerateATObjectSource(this.baseNamespace, modelClasses);
        var atRecordPath = Path.Combine(this.basePath, "ATObject.g.cs");
        await File.WriteAllTextAsync(atRecordPath, atRecordSource);
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
            foreach (var group in groupList)
            {
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
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// {group.Key.ToLower()} Endpoint Group.");
                sb.AppendLine($"        /// </summary>");
                sb.AppendLine($"        public {this.baseNamespace}.{group.Key}.{className} {className.Replace("ATProto", string.Empty).Replace("Bluesky", string.Empty)} => new (this);");
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

    private void CreateAuthenticationCheck(StringBuilder sb, int spaces = 12)
    {
        sb.AppendLine($"{new string(' ', spaces)}if (!atp.IsAuthenticated)");
        sb.AppendLine($"{new string(' ', spaces)}{{");
        sb.AppendLine($"{new string(' ', spaces + 4)}throw new FFAuthenticationError();");
        sb.AppendLine($"{new string(' ', spaces)}}}");
        sb.AppendLine();
    }

    private async Task GenerateEndpointClassAsync(IGrouping<string, ClassGeneration> group)
    {
            Console.WriteLine($"Generating Endpoint Group: {group.Key}");
            var sb = new StringBuilder();
            this.GenerateHeader(sb);
            this.GenerateNamespace(sb, $"{this.baseNamespace}.{group.Key}");
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
                var inputProperties = this.FetchInputProperties(item);
                var outputProperty = this.FetchOutputProperties(item);
                var sourceContext = outputProperty == "Success" ? "Success" : $"{item.CSharpNamespace.Replace(".", string.Empty)}{outputProperty.Split(".").Last()}";
                var description = !string.IsNullOrEmpty(item.Definition.Description) ? item.Definition.Description : $"Generated endpoint for {item.Id}";
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// {description}");
                sb.AppendLine($"        /// </summary>");
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

            var id = group.Key.ToLowerInvariant();
            switch (id)
            {
                case "com.atproto.repo":
                    // this.GenerateATProtocolRepoHelperMethods(sb);
                    break;
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

        var createRecord = AllClasses.First(n => n.Id == "com.atproto.repo.createRecord");
        var deleteRecord = AllClasses.First(n => n.Id == "com.atproto.repo.deleteRecord");
        var putRecord = AllClasses.First(n => n.Id == "com.atproto.repo.putRecord");
        var listRecords = AllClasses.First(n => n.Id == "com.atproto.repo.listRecords");
        var sb = new StringBuilder();
        this.GenerateHeader(sb);
        sb.AppendLine($"using FishyFlip.Lexicon.Com.Atproto.Repo;");
        sb.AppendLine();
        this.GenerateNamespace(sb, $"FishyFlip");
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
                    sb.Append($"        public static Task<Result<CreateRecordOutput?>> Create{item.ClassName}RecordAsync(");
                    var inputProperties = this.FetchInputProperties(createRecord, true);
                    inputProperties[0] = $"this {this.baseNamespace}.{item.CSharpNamespace}.{className} atp";
                    var collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    inputProperties[inputProperties.IndexOf("ATObject record")] = $"{this.baseNamespace}.{item.FullClassName} record";
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(createRecord);
                    var properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
                    // replace collection with the first input property.
                    properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
                    sb.AppendLine(")");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            return atp.ATProtocol.CreateRecordAsync({string.Join(", ", properties)});");
                    sb.AppendLine("        }");
                    sb.AppendLine();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($"        /// Delete a {item.ClassName} record.");
                    sb.AppendLine("        /// </summary>");
                    sb.Append($"        public static Task<Result<DeleteRecordOutput?>> Delete{item.ClassName}RecordAsync(");
                    inputProperties = this.FetchInputProperties(deleteRecord, true);
                    collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(deleteRecord);
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
                    sb.Append($"        public static Task<Result<PutRecordOutput?>> Put{item.ClassName}RecordAsync(");
                    inputProperties = this.FetchInputProperties(putRecord, true);
                    collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    inputProperties[inputProperties.IndexOf("ATObject record")] = $"{this.baseNamespace}.{item.FullClassName} record";
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(putRecord);
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
                    sb.Append($"        public static Task<Result<ListRecordsOutput?>> List{item.ClassName}RecordsAsync(");
                    inputProperties = this.FetchInputProperties(listRecords, true);
                    collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(listRecords);
                    properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
                    properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
                    sb.AppendLine(")");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            return atp.ListRecordsAsync({string.Join(", ", properties)});");
                    sb.AppendLine("        }");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        var outputPath = Path.Combine(this.basePath, group.Key.Replace('.', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(outputPath);
        var classPath = Path.Combine(outputPath, $"{group.Key.Split(".").Last()}Extensions.g.cs");
        if (File.Exists(classPath))
        {
            throw new Exception($"File already exists: {group.Key.Split(".").Last()}Extensions {classPath}");
        }

        await File.WriteAllTextAsync(classPath, sb.ToString());
    }

    private async Task GenerateRecordStaticExtensionClass(IGrouping<string, ClassGeneration> group)
    {
        var createRecord = AllClasses.First(n => n.Id == "com.atproto.repo.createRecord");
        var deleteRecord = AllClasses.First(n => n.Id == "com.atproto.repo.deleteRecord");
        var putRecord = AllClasses.First(n => n.Id == "com.atproto.repo.putRecord");
        var listRecords = AllClasses.First(n => n.Id == "com.atproto.repo.listRecords");
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
                    sb.Append($"        public static Task<Result<CreateRecordOutput?>> Create{item.ClassName}RecordAsync(");
                    var inputProperties = this.FetchInputProperties(createRecord, true);
                    var collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    inputProperties[inputProperties.IndexOf("ATObject record")] = $"{item.FullClassName} record";
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(createRecord);
                    var properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
                    // replace collection with the first input property.
                    properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
                    sb.AppendLine(")");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            return atp.CreateRecordAsync({string.Join(", ", properties)});");
                    sb.AppendLine("        }");
                    sb.AppendLine();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($"        /// Delete a {item.ClassName} record.");
                    sb.AppendLine("        /// </summary>");
                    sb.Append($"        public static Task<Result<DeleteRecordOutput?>> Delete{item.ClassName}RecordAsync(");
                    inputProperties = this.FetchInputProperties(deleteRecord, true);
                    collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(deleteRecord);
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
                    sb.Append($"        public static Task<Result<PutRecordOutput?>> Put{item.ClassName}RecordAsync(");
                    inputProperties = this.FetchInputProperties(putRecord, true);
                    collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    inputProperties[inputProperties.IndexOf("ATObject record")] = $"{item.FullClassName} record";
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(putRecord);
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
                    sb.Append($"        public static Task<Result<ListRecordsOutput?>> List{item.ClassName}RecordsAsync(");
                    inputProperties = this.FetchInputProperties(listRecords, true);
                    collectionItem = inputProperties.First(n => n == "string collection");
                    inputProperties.Remove(collectionItem);
                    this.GenerateInputProperties(sb, inputProperties);
                    inputProperties = this.FetchInputProperties(listRecords);
                    properties = inputProperties.Select(n => n.Split(" ")[1]).ToList();
                    properties[properties.IndexOf("collection")] = $"\"{item.Id}\"";
                    sb.AppendLine(")");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            return atp.ListRecordsAsync({string.Join(", ", properties)});");
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

    private void GenerateATProtocolRepoHelperMethods(StringBuilder sb)
    {
        var graphClasses = AllClasses.Where(n => n.Definition.Type == "record").ToList();
        foreach (var item in graphClasses)
        {
            Console.WriteLine($"Generating Repo Helper Method for {item.Id}");
            sb.AppendLine();
            var methodTypes = new List<string> { "Create", "Put", $"Delete" };
            var baseMethodName = $"{item.ClassName}Async";
            var inputProperties = new List<string> { $"{item.FullClassName} input", "CancellationToken cancellationToken = default" };
            var baseOutputProperty = $"RecordOutput";
            foreach (var methodType in methodTypes)
            {
                sb.AppendLine();
                var methodName = $"{methodType}{item.ClassName}Async";
                var description = $"{methodType} record for {item.Id}.";
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// {description}");
                sb.AppendLine($"        /// </summary>");
                sb.Append($"        public Task<Result<{this.baseNamespace}.Com.Atproto.Repo.{methodType}RecordOutput?>> {methodName} (");
                this.GenerateInputProperties(sb, inputProperties);
                sb.AppendLine($")");
                sb.AppendLine("        {");
                sb.AppendLine($"            var createRecordInput = new CreateRecordInput();");
                sb.AppendLine($"            createRecordInput.Record = input;");
                sb.AppendLine($"            return this.atp.{methodType}RecordAsync(createRecordInput, cancellationToken);");
                sb.AppendLine("        }");

            }

            sb.AppendLine("   }");
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
                var inputProperties = this.FetchInputProperties(item, true);
                var outputProperty = this.FetchOutputProperties(item);
                var sourceContext = outputProperty == "Success" ? "Success" : $"{item.CSharpNamespace.Replace(".", string.Empty)}{outputProperty.Split(".").Last()}";
                var description = !string.IsNullOrEmpty(item.Definition.Description) ? item.Definition.Description : $"Generated endpoint for {item.Id}";
                sb.AppendLine($"        /// <summary>");
                sb.AppendLine($"        /// {description}");
                sb.AppendLine($"        /// </summary>");
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
                if (item.Definition.RequiresAuth)
                {
                     // this.CreateAuthenticationCheck(sb);
                }
                switch (item.Definition.Type)
                {
                    case "procedure":
                        sb.AppendLine($"            var endpointUrl = {item.ClassName}.ToString();");
                        if (inputProperties.Count <= 2)
                        {
                            sb.AppendLine($"            return atp.Client.Post<{outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);");
                        }
                        else if (inputProperties[1].Contains("StreamContent"))
                        {
                            sb.AppendLine($"            return atp.Client.Post<{outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, {inputProperties[1].Split(" ").Last()}, cancellationToken, atp.Options.Logger);");
                        }
                        else
                        {
                            sb.AppendLine($"            var inputItem = new {item.ClassName}Input();");
                            var inputSourceContext = $"{item.CSharpNamespace.Replace(".", string.Empty)}{item.ClassName}Input";
                            for (int i = 1; i < inputProperties.Count - 1; i++)
                            {
                                 var typeName = inputProperties[i].Split(" ")[0];
                                 var prop = inputProperties[i].Split(" ")[1];
                                 sb.AppendLine($"            inputItem.{prop.Replace("@", string.Empty).ToPascalCase()} = {prop};");
                            }
                            // return atp.Client.Post<FishyFlip.Lexicon.Com.Atproto.Server.CreateAccountInput?, FishyFlip.Lexicon.Com.Atproto.Server.CreateAccountOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoServerCreateAccountInput!, atp.Options.SourceGenerationContext.ComAtprotoServerCreateAccountOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
                            sb.AppendLine($"            return atp.Client.Post<{item.ClassName}Input, {outputProperty}?>(endpointUrl, atp.Options.SourceGenerationContext.{inputSourceContext}!, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);");
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
                                    sb.AppendLine($"                queryStrings.Add(string.Join(\"&\", {prop}.Select(n => \"{prop}=\" + n)));");
                                    sb.AppendLine("            }");
                                    sb.AppendLine();
                                }
                                else
                                {
                                    sb.AppendLine($"            queryStrings.Add(string.Join(\"&\", {prop}.Select(n => \"{prop}=\" + n)));");
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

                        sb.AppendLine($"            return atp.Client.Get<{outputProperty}>(endpointUrl, atp.Options.SourceGenerationContext.{sourceContext}!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);");
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
        return new[] { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while" }.Contains(propertyName) ? $"@{propertyName}" : propertyName;
    }

    private List<string> FetchInputProperties(ClassGeneration classGeneration, bool isExtensionMethod = false)
    {
        var requiredProperties = new List<string>();
        var optionalProperties = new List<string>();

        foreach (var prop in classGeneration.Definition.Input?.Schema?.Properties ?? new Dictionary<string, PropertyDefinition>())
        {
            Console.WriteLine($"Property: {prop.Key}");
            var propertyName = this.PropertyNameToCSharpSafeValue(prop.Key);
            if (prop.Value.Ref is not null)
            {
                Console.WriteLine($"Property Ref: {prop.Value.Ref}");
                var refString = prop.Value.Ref;
                var refSplit = prop.Value.Ref.Split("#");
                if (string.IsNullOrEmpty(refSplit[0]))
                {
                    refString = $"{classGeneration.Id}#{refSplit[1]}";
                }

                var classRef = FindClassFromRef(refString);

                if (classRef is not null)
                {
                    var returnType = this.GenerateReturnTypeFromClassGeneration(classRef);

                    if (!classGeneration.Definition.Input?.Schema?.Required.Contains(prop.Key) ?? false)
                    {
                        optionalProperties.Add($"{returnType} {propertyName}");
                    }
                    else
                    {
                        requiredProperties.Add($"{returnType} {propertyName}");
                    }
                    Console.WriteLine($"{returnType} {propertyName}");
                }
            }
            else if (prop.Value.Type == "array")
            {
                if (prop.Value.Items?.Ref is not null)
                {
                    Console.WriteLine($"Property Array Ref: {prop.Value.Items.Ref}");
                    var refString = prop.Value.Items.Ref;
                    var refSplit = prop.Value.Items.Ref.Split("#");
                    if (string.IsNullOrEmpty(refSplit[0]))
                    {
                        refString = $"{classGeneration.Id}#{refSplit[1]}";
                    }

                    var classRef = FindClassFromRef(refString);

                    if (classRef is not null)
                    {
                        var returnType = this.GenerateReturnTypeFromClassGeneration(classRef);
                        if (!classGeneration.Definition.Input?.Schema?.Required.Contains(prop.Key) ?? false)
                        {
                            returnType += "?";
                            propertyName += " = default";
                            optionalProperties.Add($"List<{returnType}?> {propertyName}");
                        }
                        else
                        {
                            requiredProperties.Add($"List<{returnType}?> {propertyName}");
                        }

                        Console.WriteLine($"{returnType} {propertyName}");
                    }
                }
                else
                {
                    var returnType = prop.Value.CSharpType;
                    if (!classGeneration.Definition.Input?.Schema?.Required.Contains(prop.Key) ?? false)
                    {
                        returnType += "?";
                        switch (prop.Value.Type)
                        {
                            case "integer":
                                propertyName += $" = {prop.Value.Default ?? 0}";
                                break;
                            default:
                                propertyName += " = default";
                                break;
                        }
                        optionalProperties.Add($"{returnType} {propertyName}");
                    }
                    else
                    {
                        requiredProperties.Add($"{returnType} {propertyName}");
                    }

                    Console.WriteLine($"{returnType} {propertyName}");
                }
            }
            else
            {
                var returnType = prop.Value.CSharpType;
                if (!classGeneration.Definition.Input?.Schema?.Required.Contains(prop.Key) ?? false)
                {
                    returnType += "?";
                    switch (prop.Value.Type)
                    {
                        case "integer":
                            propertyName += $" = {prop.Value.Default ?? 0}";
                            break;
                        default:
                            propertyName += " = default";
                            break;
                    }
                    optionalProperties.Add($"{returnType} {propertyName}");
                }
                else
                {
                    requiredProperties.Add($"{returnType} {propertyName}");
                }

                Console.WriteLine($"{returnType} {propertyName}");
            }
        }

        if (classGeneration.Definition.Parameters is not null)
        {
            Console.WriteLine($"Params Type: {classGeneration.Definition.Parameters.Type}");
            foreach (var param in classGeneration.Definition.Parameters.Properties)
            {
                Console.WriteLine($"Param: {param.Key}");
                if (param.Value.Description.ToLowerInvariant().Contains("deprecated"))
                {
                    Console.WriteLine($"Skipping deprecated param: {param.Key}");
                    continue;
                }

                var returnType = param.Value.CSharpType;
                var propertyName = this.PropertyNameToCSharpSafeValue(param.Key);
                if (!classGeneration.Definition.Parameters.Required.Contains(param.Key))
                {
                    returnType += "?";
                    switch (param.Value.Type)
                    {
                        case "integer":
                            propertyName += $" = {param.Value.Default ?? 0}";
                            break;
                        default:
                            propertyName += " = default";
                            break;
                    }

                    optionalProperties.Add($"{returnType} {propertyName}");
                }
                else
                {
                    requiredProperties.Add($"{returnType} {propertyName}");
                }

            }
        }

        if (classGeneration.Definition.Input?.Encoding == "*/*")
        {
            requiredProperties.Add("StreamContent content");
        }

        var inputProperties = requiredProperties.Concat(optionalProperties).ToList();
        if (isExtensionMethod)
        {
            inputProperties.Insert(0, "this FishyFlip.ATProtocol atp");
        }

        inputProperties.Add("CancellationToken cancellationToken = default");

        return inputProperties;
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
                return $"List<ATObject?>";
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
            var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(jsonText, SourceGenerationContext.Default.SchemaDocument);
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

    public static PropertyGeneration? FindPropertyFromRef(string refString)
    {
        var splitHashtag = refString.Split('#').Where(n => !string.IsNullOrEmpty(n)).ToArray();
        if (splitHashtag.Length == 2)
        {
            var def = splitHashtag[0];
            var key = splitHashtag[1];
            var cls = AllClasses.FirstOrDefault(n => n.Id == def);
            if (cls != null)
            {
                var prop = cls.Properties.FirstOrDefault(n => n.Key == key);
                if (prop != null)
                {
                    return prop;
                }
            }
        }

        return null;
    }

    private async Task ProcessFile(string defJsonPath)
    {
        var defJsonText = await File.ReadAllTextAsync(defJsonPath);
        var schemaDocument = JsonSerializer.Deserialize<SchemaDocument>(defJsonText, SourceGenerationContext.Default.SchemaDocument);
        if (schemaDocument == null)
        {
            throw new Exception($"Failed to deserialize {defJsonPath}.");
        }

        var path = Path.Combine(schemaDocument.Id.Split('.').Take(schemaDocument.Id.Split('.').Length - 1).Select(n => n.ToPascalCase()).ToArray());
        foreach (var definition in schemaDocument.Defs)
        {
            var classGeneration = new ClassGeneration(schemaDocument, definition.Key, definition.Value, path);
            Console.WriteLine(classGeneration.ToString());
            AllClasses.Add(classGeneration);
            if (classGeneration.Definition.Output is not null && classGeneration.Definition.Output.Schema?.Type == "object")
            {
                var outputClass = new ClassGeneration(classGeneration.Document, $"{classGeneration.ClassName}Output", classGeneration.Definition.Output.Schema, classGeneration.Path);
                AllClasses.Add(outputClass);
            }

            if (classGeneration.Definition.Input is not null)
            {
                var outputClass = new ClassGeneration(classGeneration.Document, $"{classGeneration.ClassName}Input", classGeneration.Definition.Input.Schema, classGeneration.Path);
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
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATWebSocketCommitTypeConverter),");
        sb.AppendLine($"            typeof(FishyFlip.Tools.Json.ATWebSocketEventConverter),");
        sb.AppendLine("        },");
        sb.AppendLine($"        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,");
        sb.AppendLine($"        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]");
        foreach (var cls in classes)
        {
            var typeName = $"{this.baseNamespace}.{cls.CSharpNamespace}.{cls.ClassName}";
            var typeInfoPropertyName = $"{cls.CSharpNamespace}.{cls.ClassName}".Replace(".", string.Empty);
            sb.AppendLine($"    [JsonSerializable(typeof({typeName}), TypeInfoPropertyName = \"{typeInfoPropertyName}\")]");
            sb.AppendLine($"    [JsonSerializable(typeof(List<{typeName}>), TypeInfoPropertyName = \"List{typeInfoPropertyName}\")]");
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
        sb.AppendLine($"    [JsonSerializable(typeof(OidcClientOptions))]");
        sb.AppendLine($"    [JsonSerializable(typeof(DPoPProofPayload))]");
        sb.AppendLine($"    [JsonSerializable(typeof(Dictionary<string, JsonElement>))]");
        sb.AppendLine($"    [JsonSerializable(typeof(Microsoft.IdentityModel.Tokens.JsonWebKey), TypeInfoPropertyName = nameof(Microsoft.IdentityModel.Tokens.JsonWebKey) + \"_A\")]");
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
        sb.AppendLine($"            return JsonSerializer.Serialize<{cls.CSharpNamespace}.{cls.ClassName}>(this, (JsonTypeInfo<{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
        sb.AppendLine("        }");
    }

    private void GenerateFromJsonWithSourceGenerator(StringBuilder sb, ClassGeneration cls)
    {
        sb.AppendLine($"        public static {cls.ClassName} FromJson(string json)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return JsonSerializer.Deserialize<{cls.CSharpNamespace}.{cls.ClassName}>(json, (JsonTypeInfo<{cls.CSharpNamespace}.{cls.ClassName}>)SourceGenerationContext.Default.{$"{cls.CSharpNamespace}".Replace(".", string.Empty)}{cls.ClassName})!;");
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

        // Add property documentation if available
        if (!string.IsNullOrEmpty(property.PropertyDefinition.Description))
        {
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {property.PropertyDefinition.Description}");
            sb.AppendLine($"        /// </summary>");
        }

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
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATUriJsonConverter>))]");
            }
            else if (property.RawType == "Ipfs.Cid?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATCidJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATHandle?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATHandleJsonConverter>))]");
            }
            else if (property.RawType == "FishyFlip.Models.ATDid?")
            {
                sb.AppendLine($"        [JsonConverter(typeof(FishyFlip.Tools.Json.GenericListConverter<{property.RawType}, FishyFlip.Tools.Json.ATDidJsonConverter>))]");
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

        // Handle default values
        if (property.PropertyDefinition.Default != null)
        {
            sb.AppendLine($"        public {property.Type} {property.PropertyName} {{ get; set; }} = {property.GetDefaultValue()};");
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
        sb.AppendLine("        public static ATObject? ToATObject(this CBORObject obj)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (obj == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                return null;");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine("            var type = obj[\"$type\"].AsString();");
        sb.AppendLine("            switch (type)");
        sb.AppendLine("            {");
        foreach (var cls in classes)
        {
            sb.AppendLine($"                case \"{cls.Id}\":");
            sb.AppendLine($"                    return new {cls.CSharpNamespace}.{cls.ClassName}(obj);");
        }

        sb.AppendLine("                default:");
        sb.AppendLine("                    return null;");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        public static List<ATObject?>? ToATObjectList(this CBORObject obj)");
        sb.AppendLine("        {");
        sb.AppendLine("            if (obj == null)");
        sb.AppendLine("            {");
        sb.AppendLine("                return null;");
        sb.AppendLine("            }");
        sb.AppendLine();
        sb.AppendLine("            var list = new List<ATObject?>();");
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
            sb.AppendLine($"    [JsonDerivedType(typeof({cls.CSharpNamespace}.{cls.ClassName}), typeDiscriminator: \"{cls.Id}\")]");
        }

        sb.AppendLine($"    [JsonDerivedType(typeof(FishyFlip.Models.Blob), typeDiscriminator: \"blob\")]");
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
        sb.AppendLine($"        public virtual string Type => throw new NotImplementedException();");
        sb.AppendLine();
        sb.AppendLine($"        public virtual string ToJson()");
        sb.AppendLine("        {");
        sb.AppendLine($"            throw new NotImplementedException();");
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

    private void GenerateClassDocumentation(StringBuilder sb, PropertyDefinition definition)
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