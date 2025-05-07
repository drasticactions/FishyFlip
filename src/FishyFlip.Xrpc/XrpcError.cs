// <copyright file="XrpcError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace FishyFlip.Models;

/// <summary>
/// Internal error handle object.
/// Used for returning errors from the ATProtocol.
/// </summary>
internal class XrpcError
{
    /// <summary>
    /// Gets the status code.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Returns the error as a JSON string.
    /// </summary>
    /// <returns>JSON string.</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this, (JsonTypeInfo<FishyFlip.Models.XrpcError>)SourceGenerationContext.Default.XrpcError);
    }
}