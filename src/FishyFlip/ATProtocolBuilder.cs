// <copyright file="ATProtocolBuilder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public class ATProtocolBuilder
{
    private readonly ATProtocolOptions atProtocolOptions;

    public ATProtocolBuilder()
    {
        this.atProtocolOptions = new ATProtocolOptions();
    }

    /// <summary>
    /// Set a custom HttpClient.
    /// </summary>
    /// <param name="client">HttpClient.</param>
    /// <returns><see cref="ATProtocolBuilder"/>.</returns>
    public ATProtocolBuilder WithHttpClient(HttpClient client)
    {
        this.atProtocolOptions.HttpClient = client;
        return this;
    }

    /// <summary>
    /// Builds the Protocol.
    /// </summary>
    /// <returns>The <seealso cref="ATProtocol"/> build with these configs.</returns>
    public ATProtocol Build()
    {
        return new ATProtocol(this.atProtocolOptions);
    }
}