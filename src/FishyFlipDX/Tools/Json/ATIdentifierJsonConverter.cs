// <copyright file="ATIdentifierJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Json;

/// <summary>
/// Provides a converter for the ATHandle type for JSON serialization and deserialization.
/// </summary>
public class ATIdentifierJsonConverter : JsonConverter<ATIdentifier>
{
    /// <inheritdoc/>
    public override ATIdentifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return default;
        }

        try
        {
            if (ATHandle.TryCreate(value, out ATHandle? uri))
            {
                return uri;
            }

            if (ATDid.TryCreate(value, out ATDid? did))
            {
                return did;
            }
        }
        catch (Exception)
        {
            return default;
        }

        return default;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATIdentifier? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
