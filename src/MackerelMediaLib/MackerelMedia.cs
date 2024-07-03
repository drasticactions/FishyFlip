// <copyright file="MackerelMedia.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using MackerelMediaLib.Models;

namespace MackerelMediaLib;

/// <summary>
/// Mackerel Media.
/// </summary>
public sealed class MackerelMedia
{
    private ATProtocol proto;

    private SourceGenerationContext sourceGenerationContext;

    private JsonSerializerOptions jsonSerializerOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="MackerelMedia"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    public MackerelMedia(ATProtocol proto)
    {
        this.proto = proto;
        this.jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            Converters =
            {
                new AtUriJsonConverter(),
                new AtHandlerJsonConverter(),
                new AtDidJsonConverter(),
                new EmbedConverter(),
                new ATRecordJsonConverter(),
                new ATCidConverter(),
            },
        };
        this.sourceGenerationContext = new SourceGenerationContext(this.jsonSerializerOptions);
    }

    private ATProtocolOptions Options => this.proto.Options;

    /// <summary>
    /// Asynchronously uploads a media record to the repository.
    /// </summary>
    /// <param name="mediaRecord">The media record to be uploaded.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{RecordRef}"/>.</returns>
    public async Task<Result<RecordRef>> UploadMediaAsync(CreateMediaRecord mediaRecord, CancellationToken cancellationToken = default)
        => await this.proto.Repo.CreateRecord<CreateMediaRecord, RecordRef>(mediaRecord, this.sourceGenerationContext.CreateMediaRecord, this.sourceGenerationContext.RecordRef, cancellationToken);

    /// <summary>
    /// Asynchronously uploads a media file to the repository and creates a media record.
    /// </summary>
    /// <param name="mediaBytes">The byte array of the media file to be uploaded.</param>
    /// <param name="mediaHeader">The content type header of the media file.</param>
    /// <param name="thumbnail">An optional thumbnail for the media.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{RecordRef}"/>.</returns>
    public async Task<Result<RecordRef>> UploadMediaAsync(byte[] mediaBytes, MediaTypeHeaderValue mediaHeader, ImageEmbed? thumbnail, CancellationToken cancellationToken = default)
    {
        using var memoryStream = new MemoryStream(mediaBytes);
        using var streamContent = new StreamContent(memoryStream);
        streamContent.Headers.ContentType = mediaHeader;
        var (blobResult, blobError) = await this.proto.Repo.UploadBlobAsync(streamContent, cancellationToken);

        if (blobError is not null)
        {
            return blobError!;
        }

        var blob = blobResult!.Blob;
        var media = new Media(blob.MimeType, blob.Size, thumbnail)
        {
            Ref = new MediaRef(blob.Ref?.Link),
        };

        var mediaRecord = new CreateMediaRecord(Constants.MackerelMediaTypes.Media, this.proto!.Session!.Did.ToString()!, media);
        return await this.proto.Repo.CreateRecord<CreateMediaRecord, RecordRef>(mediaRecord, this.sourceGenerationContext.CreateMediaRecord, this.sourceGenerationContext.RecordRef, cancellationToken);
    }

    /// <summary>
    /// Asynchronously uploads a media file to the repository.
    /// </summary>
    /// <param name="mediaBytes">The bytes to be uploaded.</param>
    /// <param name="mediaHeader">The media header.</param>
    /// <param name="thumbnail">The thumbnail for the media. Optional.</param>
    /// <param name="thumbnailAlt">The thumbnail alt for the media. Optional.</param>
    /// <param name="cancellationToken">An optional cancellation token. Defaults to None.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result of type RecordRef.</returns>
    public async Task<Result<RecordRef>> UploadMediaAsync(byte[] mediaBytes, MediaTypeHeaderValue mediaHeader, byte[]? thumbnail = default, string? thumbnailAlt = default, CancellationToken cancellationToken = default)
    {
        ImageEmbed? thumbnailImage = null;
        if (thumbnail is null)
        {
            return await this.UploadMediaAsync(mediaBytes, mediaHeader, thumbnailImage, cancellationToken);
        }

        var (thumbnailBlobResult, thumbnailBlobError) =
            await this.proto.Repo.UploadImageAsync(thumbnail, cancellationToken);
        if (thumbnailBlobError is not null)
        {
            return thumbnailBlobError!;
        }

        var thumbnailBlob = thumbnailBlobResult!.Blob;
        thumbnailImage = new ImageEmbed(thumbnailBlob.ToImage(), thumbnailAlt);

        return await this.UploadMediaAsync(mediaBytes, mediaHeader, thumbnailImage, cancellationToken);
    }

    /// <summary>
    /// Retrieves a media record asynchronously.
    /// </summary>
    /// <param name="repo">The AT identifier of the repository.</param>
    /// <param name="rkey">The record key.</param>
    /// <param name="cid">The CID of the media record. If null, the latest record will be retrieved.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the media record, or null if not found.</returns>
    public async Task<Result<MediaRecord?>> GetMediaAsync(ATIdentifier repo, string rkey, ATCid? cid = null, CancellationToken cancellationToken = default)
        => await this.proto.Repo.GetRecordAsync<MediaRecord>(Constants.MackerelMediaTypes.Media, this.sourceGenerationContext.MediaRecord, repo, rkey, cid, cancellationToken);

    /// <summary>
    /// Asynchronously list the media from the user.
    /// </summary>
    /// <param name="userDid">User repo.</param>
    /// <param name="limit">The maximum number of records to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A string that represents the starting point for the next set of records.</param>
    /// <param name="reverse">Optional. A boolean that indicates whether the records should be retrieved in reverse order.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with a list of records, or null if no records were found.</returns>
    /// <returns>A list of author posts.</returns>
    public async Task<Result<ListRecords<Media>?>> GetMediaFromUserAsync(
        ATIdentifier userDid,
        int limit = 50,
        string? cursor = default,
        bool? reverse = default,
        CancellationToken cancellationToken = default)
    {
        var (protocol, did, usingCurrentProto) =
            await this.proto.GenerateClientFromATIdentifierAsync(userDid, cancellationToken, this.Options.Logger);

        string url =
            $"{FishyFlip.Constants.Urls.ATProtoRepo.ListRecords}?collection={Constants.MackerelMediaTypes.Media}&repo={userDid}&limit={limit}";
        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        if (reverse is not null)
        {
            url += $"&reverse={reverse}";
        }

        try
        {
            return await protocol.Client.Get<ListRecords<Media>>(url, this.sourceGenerationContext.ListRecordsMedia, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        }
        finally
        {
            if (!usingCurrentProto)
            {
                protocol.Dispose();
            }
        }
    }
}