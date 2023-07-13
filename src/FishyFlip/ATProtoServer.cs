// <copyright file="ATProtoServer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

public sealed class ATProtoServer
{
    private ATProtocol proto;

    internal ATProtoServer(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public async Task<Result<Session>> CreateSessionAsync(string identifier, string password, CancellationToken cancellationToken = default)
    {
        Result<Session> result =
            await this.Client.Post<Login, Session>(Constants.Urls.ATProtoServer.CreateSession, this.Options.JsonSerializerOptions, new Login(identifier, password), cancellationToken);

        return
            result
                .Match(
                s =>
                {
                    this.proto.OnUserLoggedIn(s);
                    return result;
                },
                error => error!);
    }

    public async Task<Result<Session>> RefreshSessionAsync(
    Session session,
    CancellationToken cancellationToken = default)
    {
        this.Client
            .DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue("Bearer", session.RefreshJwt);

        var result = await this.Client.Post<Session>(Constants.Urls.ATProtoServer.RefreshSession, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        return
            result
                .Match(
                s =>
                {
                    this.proto.SetSession(s);
                    return result;
                },
                error => error!);
    }

    public async Task<Result<InviteCode>> CreateInviteCodeAsync(int useCount, ATDid? forAccount = default, CancellationToken cancellationToken = default)
    {
       return await this.Client.Post<CreateInviteCode, InviteCode>(Constants.Urls.ATProtoServer.CreateInviteCode, this.Options.JsonSerializerOptions, new CreateInviteCode(useCount, forAccount), cancellationToken);
    }

    public async Task<Result<InviteCodes>> CreateInviteCodesAsync(int useCount, int codeCount = 1, ATDid[]? forAccounts = default, CancellationToken cancellationToken = default)
    {
        return await this.Client.Post<CreateInviteCodes, InviteCodes>(Constants.Urls.ATProtoServer.CreateInviteCode, this.Options.JsonSerializerOptions, new CreateInviteCodes(codeCount, useCount, forAccounts), cancellationToken);
    }

    public Task<Result<SessionInfo?>> GetSessionAsync(CancellationToken cancellationToken = default)
      => this.Client.Get<SessionInfo>(Constants.Urls.ATProtoServer.GetSession, this.Options.JsonSerializerOptions, cancellationToken);

    public Task<Result<AccountInviteCodes?>> GetAccountInviteCodesAsync(bool includeUsed = true, bool createAvailable = true, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoServer.GetAccountInviteCodes}?includeUsed={includeUsed.ToString().ToLowerInvariant()}&createAvailable={createAvailable.ToString().ToLowerInvariant()}";
        return this.Client.Get<AccountInviteCodes>(url, this.Options.JsonSerializerOptions, cancellationToken);
    }

    public Task<Result<AppPasswords?>> ListAppPasswordsAsync(CancellationToken cancellationToken = default)
       => this.Client.Get<AppPasswords>(Constants.Urls.ATProtoServer.ListAppPasswords, this.Options.JsonSerializerOptions, cancellationToken);

    public Task<Result<DescribeServer?>> DescribeServerAsync(CancellationToken cancellationToken = default)
        => this.Client.Get<DescribeServer>(Constants.Urls.ATProtoServer.DescribeServer, this.Options.JsonSerializerOptions, cancellationToken);
}
