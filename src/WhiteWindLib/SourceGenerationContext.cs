// <copyright file="SourceGenerationContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using WhiteWindLib.Models.Internal.WhiteWind;
using WhiteWindLib.Models.WhiteWind;

/// <summary>
/// ATProtocol Message Source Generation Context.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(EntryRecord))]
[JsonSerializable(typeof(Ogp))]
[JsonSerializable(typeof(ListRecords<Entry>))]
[JsonSerializable(typeof(CreateEntryRecord))]
[JsonSerializable(typeof(Entry))]
[JsonSerializable(typeof(BlobMetadata))]
[JsonSerializable(typeof(BlobRecord))]
[JsonSerializable(typeof(RecordRef))]
[JsonSerializable(typeof(DeleteRecord))]
[JsonSerializable(typeof(Success))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}