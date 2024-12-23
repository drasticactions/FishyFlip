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
    /// <param name="serviceType">The service type.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>Uri.</returns>
    public static Uri? GetServiceEndpointUrl(this DidDoc didDoc, string serviceType, ILogger? logger = null)
    {
        var serviceUrl = didDoc.Service?.FirstOrDefault(s => s.Id == serviceType)?.ServiceEndpoint;
        if (serviceUrl is null)
        {
            serviceUrl = didDoc.Service?.FirstOrDefault(s => s.Type == serviceType)?.ServiceEndpoint;
        }

        if (serviceUrl is null)
        {
            logger?.LogWarning($"DidDoc does not contain an endpoint for {serviceType}");
            return null;
        }

        var result = Uri.TryCreate(serviceUrl, UriKind.Absolute, out Uri? uriResult);
        if (!result)
        {
            logger?.LogWarning("DidDoc contains an invalid service endpoint.");
            return null;
        }

        return uriResult;
    }

    /// <summary>
    /// Gets the PDS endpoint URL.
    /// </summary>
    /// <param name="didDoc">The DidDoc.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>Uri.</returns>
    public static Uri? GetPDSEndpointUrl(this DidDoc didDoc, ILogger? logger = null)
    {
        var serviceUrl = didDoc.Service?.FirstOrDefault(s => s.Id == Constants.AtprotoPersonalDataServerId)?.ServiceEndpoint;
        if (serviceUrl is null)
        {
            logger?.LogWarning($"DidDoc does not contain a PDS endpoint with {Constants.AtprotoPersonalDataServerId}. Falling back to AtprotoPersonalDataServer.");
            serviceUrl = didDoc.Service?.FirstOrDefault(s => s.Type == Constants.AtprotoPersonalDataServer)?.ServiceEndpoint;
        }

        if (serviceUrl is null)
        {
            logger?.LogWarning("DidDoc does not contain a PDS endpoint.");
            return null;
        }

        var result = Uri.TryCreate(serviceUrl, UriKind.Absolute, out Uri? uriResult);
        if (!result)
        {
            logger?.LogWarning("DidDoc contains an invalid PDS endpoint.");
            return null;
        }

        return uriResult;
    }
}