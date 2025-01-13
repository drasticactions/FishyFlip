// <copyright file="OidcClientSourceGenerationContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IdentityModel.OidcClient
{
    /// <summary>
    /// Source generation context for OidcClient.
    /// </summary>
    [JsonSourceGenerationOptions(
        WriteIndented = false,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        GenerationMode = JsonSourceGenerationMode.Metadata,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(AuthorizeState))]
    [JsonSerializable(typeof(Dictionary<string, JsonElement>))]
    [JsonSerializable(typeof(OidcClientOptions))]
    internal partial class OidcClientSourceGenerationContext : JsonSerializerContext
    {
    }
}