// <copyright file="ATProtoModeration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoModeration
{
    private ATProtocol proto;

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
                       this.Options.JsonSerializerOptions,
                       new CreateModerationReportPost(reasonType.ToFriendlyString(), new RepoStrongRef(uri, cid), reason),
                       cancellationToken,
                       this.Options.Logger);
    }

    public Task<Result<ModerationRecord?>> CreateModerationReportRepoAsync(ModerationReasonType reasonType, ATDid subject, string? reason = default, CancellationToken cancellationToken = default)
    {
        return this.Client.Post<CreateModerationReportRepo, ModerationRecord?>(
                       Constants.Urls.ATProtoModeration.CreateReport,
                       this.Options.JsonSerializerOptions,
                       new CreateModerationReportRepo(reasonType.ToFriendlyString(), new AdminRepoRef(subject), reason),
                       cancellationToken,
                       this.Options.Logger);
    }
}
