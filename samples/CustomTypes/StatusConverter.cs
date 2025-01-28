using System.Text.Json;
using FishyFlip.Lexicon;
using FishyFlip.Tools.Json;

namespace CustomTypes;

/// <summary>
/// Status Converter.
/// </summary>
public class StatusConverter : ICustomATObjectConverter
{
    private readonly SourceGenerationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusConverter"/> class.
    /// </summary>
    /// <param name="context">Json Type Info for StatusConverter.</param>
    internal StatusConverter(SourceGenerationContext context)
    {
        this.context = context;
        this.SupportedTypes = new List<string> { Status.RecordType };
    }

    /// <inheritdoc/>
    public IReadOnlyList<string> SupportedTypes { get; }

    /// <inheritdoc/>
    public ATObject? Read(string text, string type, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<Status>(text, this.context.Status);
    }

    /// <inheritdoc/>
    public void Write(Utf8JsonWriter writer, ATObject value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), this.context);
    }
}