// <copyright file="ATProtoModeration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT Proto Moderation.
/// </summary>
public sealed class ATProtoModeration
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtoModeration"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal ATProtoModeration(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Creates a moderation report for a post asynchronously.
    /// </summary>
    /// <param name="reasonType">The type of the moderation reason.</param>
    /// <param name="uri">The URI of the post.</param>
    /// <param name="cid">The CID of the post.</param>
    /// <param name="reason">The reason for the moderation report. This is optional.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the operation. This is optional.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{ModerationRecord}"/> that represents the moderation record.</returns>
    public Task<Result<ModerationRecord?>> CreateModerationReportPostAsync(ModerationReasonType reasonType, ATUri uri, ATCid cid, string? reason = default, CancellationToken cancellationToken = default)
    {
        return this.Client.Post<CreateModerationReportPost, ModerationRecord?>(
                       Constants.Urls.ATProtoModeration.CreateReport,
                       this.Options.SourceGenerationContext.CreateModerationReportPost,
                       this.Options.SourceGenerationContext.ModerationRecord!,
                       this.Options.JsonSerializerOptions,
                       new CreateModerationReportPost(reasonType.ToEndpointString(), new RepoStrongRef(uri, cid), reason),
                       cancellationToken,
                       this.Options.Logger);
    }

    /// <summary>
    /// Creates a moderation report for a repository asynchronously.
    /// </summary>
    /// <param name="reasonType">The type of the moderation reason.</param>
    /// <param name="subject">The decentralized identifier (DID) of the subject.</param>
    /// <param name="reason">The reason for the moderation report. This is optional.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the operation. This is optional.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{ModerationRecord}"/> that represents the moderation record.</returns>
    public Task<Result<ModerationRecord?>> CreateModerationReportRepoAsync(ModerationReasonType reasonType, ATDid subject, string? reason = default, CancellationToken cancellationToken = default)
    {
        return this.Client.Post<CreateModerationReportRepo, ModerationRecord?>(
                       Constants.Urls.ATProtoModeration.CreateReport,
                       this.Options.SourceGenerationContext.CreateModerationReportRepo,
                       this.Options.SourceGenerationContext.ModerationRecord!,
                       this.Options.JsonSerializerOptions,
                       new CreateModerationReportRepo(reasonType.ToEndpointString(), new AdminRepoRef(subject), reason),
                       cancellationToken,
                       this.Options.Logger);
    }
}
