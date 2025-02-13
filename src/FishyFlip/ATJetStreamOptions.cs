// <copyright file="ATJetStreamOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Json;

namespace FishyFlip;

/// <summary>
/// AT JetStream Options.
/// </summary>
public class ATJetStreamOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATJetStreamOptions"/> class.
    /// </summary>
    /// <param name="customAtObjectConverters">Customer JSON Converters for ATObject.</param>
    public ATJetStreamOptions(IReadOnlyList<ICustomATObjectJsonConverter>? customAtObjectConverters = null)
    {
        this.Url = new Uri("https://jetstream.atproto.tools");
        this.Compression = false;
        this.JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            Converters = { new ATUriJsonConverter(), new ATObjectJsonConverter(customAtObjectConverters) },
        };

        this.SourceGenerationContext = new SourceGenerationContext(this.JsonSerializerOptions);
    }

    /// <summary>
    /// Gets the instance Url.
    /// </summary>
    public Uri Url { get; internal set; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    public ILogger? Logger { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether stream compression is used.
    /// </summary>
    public bool Compression { get; internal set; }

    /// <summary>
    /// Gets the zstd dictionary.
    /// </summary>
    public byte[]? Dictionary { get; internal set; }

    /// <summary>
    /// Gets the wanted collections.
    /// </summary>
    public string[] WantedCollections { get; internal set; } = Array.Empty<string>();

    /// <summary>
    /// Gets the wanted DIDs.
    /// </summary>
    public ATDid[] WantedDids { get; internal set; } = Array.Empty<ATDid>();

    /// <summary>
    /// Gets the max message size in bytes.
    /// </summary>
    public int MaxMessageSizeBytes { get; internal set; } = 0;

    /// <summary>
    /// Gets the cursor.
    /// </summary>
    public long? Cursor { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether to require a pause replay/live-tail until the server recevies a SubscriberOptionsUpdatePayload over the socket in a Subscriber Sourced Message.
    /// </summary>
    public bool RequireHello { get; internal set; } = false;

    /// <summary>
    /// Gets the JsonSerializerOptions.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; internal set; }

    /// <summary>
    /// Gets the source generation context.
    /// </summary>
    internal SourceGenerationContext SourceGenerationContext { get; }
}