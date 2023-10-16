// <copyright file="ATWebSocketProtocolBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT Protocol Builder.
/// </summary>
public class ATWebSocketProtocolBuilder
{
    private readonly ATWebSocketProtocolOptions atProtocolOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketProtocolBuilder"/> class.
    /// </summary>
    public ATWebSocketProtocolBuilder()
    {
        this.atProtocolOptions = new ATWebSocketProtocolOptions();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATWebSocketProtocolBuilder"/> class.
    /// </summary>
    /// <param name="options">ATWebSocketProtocolOptions.</param>
    public ATWebSocketProtocolBuilder(ATWebSocketProtocolOptions options)
    {
        this.atProtocolOptions = options;
    }

    /// <summary>
    /// Set the instance url to connect to.
    /// </summary>
    /// <param name="url">Instance Url.</param>
    /// <returns><see cref="ATWebSocketProtocolBuilder"/></returns>
    public ATWebSocketProtocolBuilder WithInstanceUrl(Uri url)
    {
        this.atProtocolOptions.Url = url;
        return this;
    }

    /// <summary>
    /// Adds a logger.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <returns><see cref="ATWebSocketProtocolBuilder"/></returns>
    public ATWebSocketProtocolBuilder WithLogger(ILogger? logger)
    {
        this.atProtocolOptions.Logger = logger;
        return this;
    }

    /// <summary>
    /// Returns the ATWebSocketProtocolOptions.
    /// </summary>
    /// <returns>ATWebSocketProtocolOptions</returns>
    public ATWebSocketProtocolOptions BuildOptions()
    {
        return this.atProtocolOptions;
    }

    /// <summary>
    /// Builds the Protocol.
    /// </summary>
    /// <returns>The <seealso cref="ATProtocol"/> build with these configs.</returns>
    public ATWebSocketProtocol Build()
    {
        var options = this.BuildOptions();

        return new ATWebSocketProtocol(options);
    }
}
