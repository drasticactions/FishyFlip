using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FishyFlip;

var supportedItems = new List<string>();
supportedItems.AddRange(GetItems(typeof(Constants.Urls)));
supportedItems.AddRange(GetItems(typeof(Constants.Urls.Bluesky)));

// Console.WriteLine("Enter JSON Directory: ");
// Put path to ATProtocol lexicons here
string jsonDirectory = "/Users/drasticactions/Developer/Personal/Libraries/atproto/lexicons";

var files = Directory.EnumerateFiles(jsonDirectory, "*.json", SearchOption.AllDirectories);
var baseAddress = "https://github.com/bluesky-social/atproto/blob/main/lexicons";

var stringBuilder = new StringBuilder();
stringBuilder.AppendLine("| Endpoint | Implemented");
stringBuilder.AppendLine("|----------|----------|");
foreach (var file in files)
{
    var filename = Path.GetFileName(file);
    if (filename.Contains("def.") || filename.Contains("defs."))
        continue;

    var relPath = file.Replace(jsonDirectory, "");
    var json = File.ReadAllText(file);
    var lexiconFile = JsonSerializer.Deserialize<LexiconFile>(json);
    if (lexiconFile.Definition.Main.Type == "object")
        continue;
    var supported = supportedItems.Contains(lexiconFile.Id) ? "\u2705" : "\u274c";
    var line = $"| [{lexiconFile.Id}]({baseAddress}{relPath})  | {supported}  |";
    stringBuilder.AppendLine(line);
}

File.WriteAllText("output.md", stringBuilder.ToString());

List<string> GetItems(Type urlsType)
{
    List<string> items = new List<string>();
    foreach (Type nestedClass in urlsType.GetNestedTypes())
    {
        Console.WriteLine($"Nested Class: {nestedClass.Name}");
        FieldInfo[] fields = nestedClass.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(string) && field.IsLiteral)
            {
                string value = field.GetValue(null).ToString();
                Console.WriteLine($"Field: {field.Name}, Value: {value}");
                if (!string.IsNullOrEmpty(value))
                {
                    items.Add(value.Split("/xrpc/").LastOrDefault() ?? value);
                }
            }
        }
    }

    return items;
}

public class LexiconFile
{
    [JsonPropertyName("id")] public string Id { get; set; }
    
    [JsonPropertyName("defs")] public LexiconDefinition Definition { get; set; }
}

public class LexiconDefinition
{
    [JsonPropertyName("main")] public MainLexiconDefinition Main { get; set; }
}

public class MainLexiconDefinition
{
    [JsonPropertyName("type")] public string Type { get; set; }
    
    [JsonPropertyName("description")] public string Description { get; set; }
}