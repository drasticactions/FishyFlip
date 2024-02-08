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

    public Task<Result<ModerationRecord?>> CreateModerationReportPostAsync(ModerationReasonType reasonType, ATUri uri, Cid cid, string? reason = default, CancellationToken cancellationToken = default)
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
