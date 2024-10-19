// <copyright file="ATWebSocketEventConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Json;

/// <summary>
/// AT WebSocket Event Converter.
/// </summary>
public class ATWebSocketEventConverter : JsonConverter<ATWebSocketEvent>
{
    /// <inheritdoc/>
    public override ATWebSocketEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (value is null)
        {
            return ATWebSocketEvent.Unknown;
        }

        switch (value)
        {
            case "com":
                return ATWebSocketEvent.Commit;
            case "acc":
                return ATWebSocketEvent.Account;
            case "id":
                return ATWebSocketEvent.Identity;
            default:
                return ATWebSocketEvent.Unknown;
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ATWebSocketEvent value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLower());
    }
}