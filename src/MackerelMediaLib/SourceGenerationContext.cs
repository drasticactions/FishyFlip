// <copyright file="SourceGenerationContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using MackerelMediaLib.Models;

/// <summary>
/// ATProtocol Message Source Generation Context.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(BlobRecord))]
[JsonSerializable(typeof(RecordRef))]
[JsonSerializable(typeof(DeleteRecord))]
[JsonSerializable(typeof(Success))]
[JsonSerializable(typeof(CreateMediaRecord))]
[JsonSerializable(typeof(Media))]
[JsonSerializable(typeof(MediaRef))]
[JsonSerializable(typeof(ListRecords<Media>))]
[JsonSerializable(typeof(MediaRecord))]
[JsonSerializable(typeof(MediaEmbed))]
[JsonSerializable(typeof(Cid))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}