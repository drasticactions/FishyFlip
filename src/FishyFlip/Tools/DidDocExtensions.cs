// <copyright file="DidDocExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// DidDoc extensions.
/// </summary>
public static class DidDocExtensions
{
    /// <summary>
    /// Gets the service endpoint URL.
    /// </summary>
    /// <param name="didDoc">The DidDoc.</param>
    /// <returns>Uri.</returns>
    public static Uri? GetServiceEndpointUrl(this DidDoc didDoc)
    {
        var serviceUrl = didDoc.Service?.FirstOrDefault()?.ServiceEndpoint;
        if (serviceUrl is null)
        {
            return null;
        }

        var result = Uri.TryCreate(serviceUrl, UriKind.Absolute, out Uri? uriResult);
        if (!result)
        {
            return null;
        }

        return uriResult;
    }
}