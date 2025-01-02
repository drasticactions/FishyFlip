// <copyright file="ATJetStreamBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    public ATJetStreamBuilder()
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
    /// Enables stream compression.
    /// </summary>
    /// <returns><see cref="ATJetStreamBuilder"/>.</returns>
    public ATJetStreamBuilder WithCompression()
    {
        this.atProtocolOptions.Compression = true;
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