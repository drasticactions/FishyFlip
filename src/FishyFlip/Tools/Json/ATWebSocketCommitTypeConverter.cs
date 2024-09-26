// <copyright file="ATWebSocketCommitTypeConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// AT WebSocket Event Converter.
/// </summary>
public class ATWebSocketCommitTypeConverter : JsonConverter<ATWebSocketCommitType>
{
    /// <inheritdoc/>
    public override ATWebSocketCommitType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return ATWebSocketCommitType.Unknown;
        }

        switch (value)
        {
            case "u":
                return ATWebSocketCommitType.Update;
            case "c":
                return ATWebSocketCommitType.Create;
            case "d":
                return ATWebSocketCommitType.Delete;
            default:
                return ATWebSocketCommitType.Unknown;
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATWebSocketCommitType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLower());
    }
}