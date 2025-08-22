// <copyright file="UnauthenticatedXrpcClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Unauthenticated XRPC Client.
/// </summary>
public class UnauthenticatedXrpcClient : BasicXrpcClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthenticatedXrpcClient"/> class.
    /// </summary>
    /// <param name="options"><see cref="ATProtocolOptions"/>.</param>
    public UnauthenticatedXrpcClient(ATProtocolOptions options)
        : base(options)
    {
        this.Client.BaseAddress = options.Url;
    }
}