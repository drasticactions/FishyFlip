// <copyright file="JsonTypeReorderer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace FishyFlip.Tools;

/// <summary>
/// Reorders the $type property in JSON objects to be the first property.
/// </summary>
internal static class JsonTypeReorderer
{
    /// <summary>
    /// Reorders the $type property in JSON objects to be the first property.
    /// </summary>
    /// <param name="json">Json.</param>
    /// <returns>Reordered Json.</returns>
    public static string ReorderTypeProperty(string json)
    {
        // Parse the JSON into a JsonNode
        JsonNode? jsonNode = JsonNode.Parse(json);
        if (jsonNode == null)
        {
            return json;
        }

        // Process the node recursively
        ProcessNode(jsonNode);

        // Convert back to string with preserved formatting
        var options = new JsonSerializerOptions
        {
            WriteIndented = false,
        };
        return jsonNode.ToJsonString(options);
    }

    /// <summary>
    /// Fixes multiple $type properties in JSON objects.
    /// Tries to work around https://github.com/dotnet/runtime/issues/92780.
    /// </summary>
    /// <param name="node">JsonNode.</param>
    /// <returns>string.</returns>
    public static string RemoveDuplicateTypeLines(this JsonNode node)
    {
        var input = node.ToJsonString(SourceGenerationContext.Default.Options);

        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var result = new StringBuilder();
        string? previousLine = null;

        foreach (var line in lines)
        {
            if (line.Contains("\"$type\"") && line == previousLine)
            {
                continue;
            }

            result.AppendLine(line);
            previousLine = line;
        }

        return result.ToString();
    }

    private static void ProcessNode(JsonNode node)
    {
        if (node is JsonObject obj)
        {
            // If this object has a $type property, ensure it's first
            if (obj.ContainsKey("$type"))
            {
                var properties = obj.ToDictionary(p => p.Key, p => p.Value?.DeepClone());
                obj.Clear();

                // Add $type first
                obj.Add("$type", properties["$type"]);

                // Add all other properties
                foreach (var prop in properties.Where(p => p.Key != "$type"))
                {
                    obj.Add(prop.Key, prop.Value);
                }
            }

            // Process all child objects recursively
            foreach (var prop in obj)
            {
                if (prop.Value != null)
                {
                    ProcessNode(prop.Value);
                }
            }
        }
        else if (node is JsonArray array)
        {
            // Process all array elements recursively
            foreach (var item in array)
            {
                if (item != null)
                {
                    ProcessNode(item);
                }
            }
        }
    }
}

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1600 // Elements should be documented
internal static class JsonNodeExtensions
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1402 // File may only contain a single type
{
    /// <summary>
    /// Deep clone a JsonNode.
    /// </summary>
    /// <param name="node">Node.</param>
    /// <returns>Json Node.</returns>
    public static JsonNode? DeepClone(this JsonNode? node)
    {
        if (node == null)
        {
            return null;
        }

        return JsonNode.Parse(node.ToJsonString());
    }
}