// <copyright file="ATProtoServer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// AT Proto Server.
/// </summary>
public sealed class ATProtoServer
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtoServer"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal ATProtoServer(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Asynchronously creates a new session.
    /// </summary>
    /// <param name="identifier">The identifier of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session details, or null if the session could not be created.</returns>
    public async Task<Result<Session>> CreateSessionAsync(string identifier, string password, CancellationToken cancellationToken = default)
    {
        Result<Session> result =
            await this.Client.Post<Login, Session>(Constants.Urls.ATProtoServer.CreateSession, this.Options.SourceGenerationContext.Login, this.Options.SourceGenerationContext.Session, this.Options.JsonSerializerOptions, new Login(identifier, password), cancellationToken);

        return
            result
                .Match(
                s =>
                {
                    return result;
                },
                error => error!);
    }

    /// <summary>
    /// Asynchronously refreshes an existing session.
    /// </summary>
    /// <param name="session">The current session that needs to be refreshed.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the refreshed session details, or null if the session could not be refreshed.</returns>
    public async Task<Result<Session>> RefreshSessionAsync(
        Session session,
        CancellationToken cancellationToken = default)
    {
        this.Client
            .DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue("Bearer", session.RefreshJwt);

        var result = await this.Client.Post<Session>(Constants.Urls.ATProtoServer.RefreshSession, this.Options.SourceGenerationContext.Session, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
        return
            result
                .Match(
                s =>
                {
                    // Used for passwords, this should be set in PasswordSessionManager.
                    // this.proto.SetSession(s);
                    return result;
                },
                error => error!);
    }

    /// <summary>
    /// Asynchronously creates a new invite code.
    /// </summary>
    /// <param name="useCount">The number of times the invite code can be used.</param>
    /// <param name="forAccount">Optional. The ATDid of the account for which the invite code is created.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the invite code details, or null if the invite code could not be created.</returns>
    public async Task<Result<InviteCode>> CreateInviteCodeAsync(int useCount, ATDid? forAccount = default, CancellationToken cancellationToken = default)
    {
        return await this.Client.Post<CreateInviteCode, InviteCode>(Constants.Urls.ATProtoServer.CreateInviteCode, this.Options.SourceGenerationContext.CreateInviteCode, this.Options.SourceGenerationContext.InviteCode, this.Options.JsonSerializerOptions, new CreateInviteCode(useCount, forAccount), cancellationToken);
    }

    /// <summary>
    /// Asynchronously creates multiple invite codes.
    /// </summary>
    /// <param name="useCount">The number of times each invite code can be used.</param>
    /// <param name="codeCount">Optional. The number of invite codes to create. Default is 1.</param>
    /// <param name="forAccounts">Optional. An array of ATDid for the accounts for which the invite codes are created.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the invite codes details, or null if the invite codes could not be created.</returns>
    public async Task<Result<InviteCodes>> CreateInviteCodesAsync(int useCount, int codeCount = 1, ATDid[]? forAccounts = default, CancellationToken cancellationToken = default)
    {
        return await this.Client.Post<CreateInviteCodes, InviteCodes>(Constants.Urls.ATProtoServer.CreateInviteCode, this.Options.SourceGenerationContext.CreateInviteCodes, this.Options.SourceGenerationContext.InviteCodes, this.Options.JsonSerializerOptions, new CreateInviteCodes(codeCount, useCount, forAccounts), cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves the current session information.
    /// </summary>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the session information, or null if no session information was found.</returns>
    public Task<Result<SessionInfo?>> GetSessionAsync(CancellationToken cancellationToken = default)
      => this.Client.Get<SessionInfo>(Constants.Urls.ATProtoServer.GetSession, this.Options.SourceGenerationContext.SessionInfo, this.Options.JsonSerializerOptions, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the invite codes for the current account.
    /// </summary>
    /// <param name="includeUsed">Optional. A boolean that indicates whether to include used invite codes. Default is true.</param>
    /// <param name="createAvailable">Optional. A boolean that indicates whether to create available invite codes. Default is true.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the invite codes for the account, or null if no invite codes were found.</returns>
    public Task<Result<AccountInviteCodes?>> GetAccountInviteCodesAsync(bool includeUsed = true, bool createAvailable = true, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.ATProtoServer.GetAccountInviteCodes}?includeUsed={includeUsed.ToString().ToLowerInvariant()}&createAvailable={createAvailable.ToString().ToLowerInvariant()}";
        return this.Client.Get<AccountInviteCodes>(url, this.Options.SourceGenerationContext.AccountInviteCodes, this.Options.JsonSerializerOptions, cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves the list of application passwords.
    /// </summary>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the list of application passwords, or null if no application passwords were found.</returns>
    public Task<Result<AppPasswords?>> ListAppPasswordsAsync(CancellationToken cancellationToken = default)
       => this.Client.Get<AppPasswords>(Constants.Urls.ATProtoServer.ListAppPasswords, this.Options.SourceGenerationContext.AppPasswords, this.Options.JsonSerializerOptions, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the server description.
    /// </summary>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the server description, or null if no server description was found.</returns>
    public Task<Result<DescribeServer?>> DescribeServerAsync(CancellationToken cancellationToken = default)
        => this.Client.Get<DescribeServer>(Constants.Urls.ATProtoServer.DescribeServer, this.Options.SourceGenerationContext.DescribeServer, this.Options.JsonSerializerOptions, cancellationToken);
}
