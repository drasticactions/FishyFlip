// <copyright file="ATJetStreamBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Tools.Json;

namespace FishyFlip;

/// <summary>
/// AT JetStream Builder.
/// </summary>
public class ATJetStreamBuilder
{
    private readonly ATJetStreamOptions atProtocolOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATJetStreamBuilder"/> class.
    /// </summary>
    /// <param name="customAtObjectConverters">Customer JSON Converters for ATObject.</param>
    public ATJetStreamBuilder(IReadOnlyList<ICustomATObjectJsonConverter>? customAtObjectConverters = null)
    {
        this.atProtocolOptions = new ATJetStreamOptions();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATJetStreamBuilder"/> class.
    /// </summary>
    /// <param name="options">ATJetStreamOptions.</param>
    public ATJetStreamBuilder(ATJetStreamOptions options)
    {
        this.atProtocolOptions = options;
    }

    /// <summary>
    /// Set the instance url to connect to.
    /// Alternatives are listed at https://github.com/bluesky-social/jetstream/tree/main?tab=readme-ov-file#public-instances.
    /// Defaults to https://jetstream1.us-east.bsky.network.
    /// </summary>
    /// <param name="url">Instance Url.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithInstanceUrl(Uri url)
    {
        this.atProtocolOptions.Url = url;
        return this;
    }

    /// <summary>
    /// Adds a logger.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithLogger(ILogger? logger)
    {
        this.atProtocolOptions.Logger = logger;
        return this;
    }

    /// <summary>
    /// <para>Enables stream compression.</para>
    /// Requires a valid copy of the zstd dictionary to function, you can find it here: <see href="https://github.com/bluesky-social/jetstream/blob/main/pkg/models/zstd_dictionary"/>.
    /// </summary>
    /// <param name="dictionary">zstd dictionary used for decompression.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithCompression(byte[] dictionary)
    {
        this.atProtocolOptions.Compression = true;
        this.atProtocolOptions.Dictionary = dictionary;
        return this;
    }

    /// <summary>
    /// Sets the wanted collections, up to 100.
    /// </summary>
    /// <param name="collections">NSID Collection types.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithWantedCollections(params string[] collections)
    {
        if (collections.Length > 100)
        {
            throw new ArgumentOutOfRangeException("Collections cannot exceed 100.");
        }

        this.atProtocolOptions.WantedCollections = collections;
        return this;
    }

    /// <summary>
    /// Sets the wanted DIDs, up to 10,000.
    /// </summary>
    /// <param name="dids">ATDids.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithWantedDids(params ATDid[] dids)
    {
        if (dids.Length > 10000)
        {
            throw new ArgumentOutOfRangeException("DIDs cannot exceed 10,000.");
        }

        this.atProtocolOptions.WantedDids = dids;
        return this;
    }

    /// <summary>
    /// Sets the max message size in bytes.
    /// </summary>
    /// <param name="maxMessageSizeBytes">Max message size in bytes.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder SetMaxMessageSizeBytes(int maxMessageSizeBytes)
    {
        this.atProtocolOptions.MaxMessageSizeBytes = maxMessageSizeBytes;
        return this;
    }

    /// <summary>
    /// Sets the require hello value.
    /// </summary>
    /// <param name="requireHello">Require Hello.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder SetRequireHello(bool requireHello)
    {
        this.atProtocolOptions.RequireHello = requireHello;
        return this;
    }

    /// <summary>
    /// Sets the cursor.
    /// </summary>
    /// <param name="cursor">Cursor.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithCursor(long cursor)
    {
        this.atProtocolOptions.Cursor = cursor;
        return this;
    }

    /// <summary>
    /// Sets a custom task factory.
    /// </summary>
    /// <param name="taskFactory">The Task Factory.</param>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithTaskFactory(TaskFactory taskFactory)
    {
        this.atProtocolOptions.TaskFactory = taskFactory;
        return this;
    }

    /// <summary>
    /// Returns the ATWebSocketProtocolOptions.
    /// </summary>
    /// <returns>ATJetStreamBuilder.</returns>
    public ATJetStreamOptions BuildOptions()
    {
        return this.atProtocolOptions;
    }

    /// <summary>
    /// Builds the Protocol.
    /// </summary>
    /// <returns>The <seealso cref="ATProtocol"/> build with these configs.</returns>
    public ATJetStream Build()
    {
        var options = this.BuildOptions();

        return new ATJetStream(options);
    }
}