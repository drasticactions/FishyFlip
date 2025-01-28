using System.Text.Json.Serialization;

namespace CustomTypes;

/// <summary>
/// ATProtocol Message Source Generation Context.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(Status))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}