// <copyright file="FacetJsonConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;
using FishyFlip.Models;

namespace FishyFlip.Tools.Json;

public class FacetJsonConverter : JsonConverter<Facet?>
{
    public FacetJsonConverter()
    {
    }

    public override bool CanConvert(Type type)
    {
        return type.IsAssignableFrom(typeof(Facet));
    }

    public override Facet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            if (doc.RootElement.TryGetProperty("index", out var index))
            {
                var byteSlice = JsonSerializer.Deserialize<ByteSlice>(index.GetRawText(), options);
                if (byteSlice is null)
                {
                    return null;
                }

                var facet = new Facet(byteSlice);

                if (doc.RootElement.TryGetProperty("features", out var type))
                {
                    foreach (JsonElement element in type.EnumerateArray())
                    {
                        if (element.TryGetProperty("$type", out var featureType))
                        {
                            switch (featureType.GetString())
                            {
                                case Constants.FacetTypes.Mention:
                                    facet.Features.Add(JsonSerializer.Deserialize<Mention>(element.GetRawText(), options)!);
                                    break;
                                case Constants.FacetTypes.Link:
                                    facet.Features.Add(JsonSerializer.Deserialize<Link>(element.GetRawText(), options)!);
                                    break;
                                default:
#if DEBUG
                                    throw new NotImplementedException();
#else
                                    break;
#endif
                            }

                            // TODO: Add more types.
                        }
                    }
                }

                return facet;
            }

            return null;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, Facet? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            return;
        }

        writer.WriteStartObject();
        writer.WriteStartObject("index");
        writer.WriteNumber("byteStart", value.Index.ByteStart);
        writer.WriteNumber("byteEnd", value.Index.ByteEnd);
        writer.WriteEndObject();
        writer.WriteStartArray("features");
        foreach (var item in value.Features)
        {
            if (item is Link link)
            {
                JsonSerializer.Serialize(writer, link, options);
            }
            else if (item is Mention mention)
            {
                JsonSerializer.Serialize(writer, mention, options);
            }
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}