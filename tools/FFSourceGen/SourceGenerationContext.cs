// <copyright file="SourceGenerationContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using FFSourceGen.Models;

namespace FFSourceGen;

/// <summary>
/// Source Generation Context.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Metadata,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(ErrorDefinition))]
[JsonSerializable(typeof(OutputDefinition))]
[JsonSerializable(typeof(ParametersDefinition))]
[JsonSerializable(typeof(PropertyDefinition))]
[JsonSerializable(typeof(SchemaDefinition))]
[JsonSerializable(typeof(SchemaDocument))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}