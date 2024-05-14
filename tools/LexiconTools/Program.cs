using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FishyFlip;

var supportedItems = new List<string>();
supportedItems.AddRange(GetItems(typeof(Constants.Urls)));
supportedItems.AddRange(GetItems(typeof(Constants.Urls.WhiteWind)));
supportedItems.AddRange(GetItems(typeof(Constants.Urls.Ozone)));
supportedItems.AddRange(GetItems(typeof(Constants.Urls.Bluesky)));
supportedItems.AddRange(GetItems(typeof(Constants.Urls.Bluesky.Chat)));
// Console.WriteLine("Enter JSON Directory: ");
// Put path to ATProtocol lexicons here
string jsonDirectory = "/Users/drasticactions/Developer/Library/atproto/lexicons";

var files = Directory.EnumerateFiles(jsonDirectory, "*.json", SearchOption.AllDirectories);
var baseAddress = "https://github.com/bluesky-social/atproto/blob/main/lexicons";

var stringBuilder = new StringBuilder();
var codeBuilder = new StringBuilder();
TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
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
    var name = lexiconFile.Id.Split(".").Last();
    var line = $"| [{lexiconFile.Id}]({baseAddress}{relPath})  | {supported}  |";
    codeBuilder.AppendLine($"public const string {name.FirstCharToUpper()} = \"/xrpc/{lexiconFile.Id}\";");
    stringBuilder.AppendLine(line);
}

File.WriteAllText("output.md", stringBuilder.ToString());
File.WriteAllText("code.md", codeBuilder.ToString());

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

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
}