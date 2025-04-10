// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Admin
{

    /// <summary>
    /// com.atproto.admin Endpoint Class.
    /// </summary>
    public sealed class ATProtoAdmin
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ATProtoAdmin"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ATProtoAdmin(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Delete a user account as an administrator.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> DeleteAccountAsync (FishyFlip.Models.ATDid did, CancellationToken cancellationToken = default)
        {
            return atp.DeleteAccountAsync(did, cancellationToken);
        }


        /// <summary>
        /// Disable an account from receiving new invite codes, but does not invalidate existing codes.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="note">Optional reason for disabled invites.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> DisableAccountInvitesAsync (FishyFlip.Models.ATDid account, string? note = default, CancellationToken cancellationToken = default)
        {
            return atp.DisableAccountInvitesAsync(account, note, cancellationToken);
        }


        /// <summary>
        /// Disable some set of codes and/or all codes associated with a set of users.
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="accounts"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> DisableInviteCodesAsync (List<string>? codes = default, List<string>? accounts = default, CancellationToken cancellationToken = default)
        {
            return atp.DisableInviteCodesAsync(codes, accounts, cancellationToken);
        }


        /// <summary>
        /// Re-enable an account's ability to receive invite codes.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="note">Optional reason for enabled invites.</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> EnableAccountInvitesAsync (FishyFlip.Models.ATDid account, string? note = default, CancellationToken cancellationToken = default)
        {
            return atp.EnableAccountInvitesAsync(account, note, cancellationToken);
        }


        /// <summary>
        /// Get details about an account.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.AccountView?>> GetAccountInfoAsync (FishyFlip.Models.ATDid did, CancellationToken cancellationToken = default)
        {
            return atp.GetAccountInfoAsync(did, cancellationToken);
        }


        /// <summary>
        /// Get details about some accounts.
        /// </summary>
        /// <param name="dids"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.GetAccountInfosOutput?>> GetAccountInfosAsync (List<FishyFlip.Models.ATDid> dids, CancellationToken cancellationToken = default)
        {
            return atp.GetAccountInfosAsync(dids, cancellationToken);
        }


        /// <summary>
        /// Get an admin view of invite codes.
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.GetInviteCodesOutput?>> GetInviteCodesAsync (string? sort = default, int? limit = 100, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetInviteCodesAsync(sort, limit, cursor, cancellationToken);
        }

        /// <summary>
        /// Get an admin view of invite codes.
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        public GetInviteCodesOutputCollection GetInviteCodesCollectionAsync (string? sort = default, int? limit = 100, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return new GetInviteCodesOutputCollection(atp, sort, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Get the service-specific admin status of a subject (account, record, or blob).
        /// </summary>
        /// <param name="did"></param>
        /// <param name="uri"></param>
        /// <param name="blob"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.GetSubjectStatusOutput?>> GetSubjectStatusAsync (FishyFlip.Models.ATDid? did = default, FishyFlip.Models.ATUri? uri = default, string? blob = default, CancellationToken cancellationToken = default)
        {
            return atp.GetSubjectStatusAsync(did, uri, blob, cancellationToken);
        }


        /// <summary>
        /// Get list of accounts that matches your search query.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.SearchAccountsOutput?>> SearchAccountsAsync (string? email = default, string? cursor = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return atp.SearchAccountsAsync(email, cursor, limit, cancellationToken);
        }

        /// <summary>
        /// Get list of accounts that matches your search query.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cursor"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public SearchAccountsOutputCollection SearchAccountsCollectionAsync (string? email = default, string? cursor = default, int? limit = 50, CancellationToken cancellationToken = default)
        {
            return new SearchAccountsOutputCollection(atp, email, cursor, limit, cancellationToken);
        }


        /// <summary>
        /// Send email to a user's account email address.
        /// </summary>
        /// <param name="recipientDid"></param>
        /// <param name="content"></param>
        /// <param name="senderDid"></param>
        /// <param name="subject"></param>
        /// <param name="comment">Additional comment by the sender that won't be used in the email itself but helpful to provide more context for moderators/reviewers</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.SendEmailOutput?>> SendEmailAsync (FishyFlip.Models.ATDid recipientDid, string content, FishyFlip.Models.ATDid senderDid, string? subject = default, string? comment = default, CancellationToken cancellationToken = default)
        {
            return atp.SendEmailAsync(recipientDid, content, senderDid, subject, comment, cancellationToken);
        }


        /// <summary>
        /// Administrative action to update an account's email.
        /// </summary>
        /// <param name="account">The handle or DID of the repo.</param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UpdateAccountEmailAsync (FishyFlip.Models.ATIdentifier account, string email, CancellationToken cancellationToken = default)
        {
            return atp.UpdateAccountEmailAsync(account, email, cancellationToken);
        }


        /// <summary>
        /// Administrative action to update an account's handle.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UpdateAccountHandleAsync (FishyFlip.Models.ATDid did, FishyFlip.Models.ATHandle handle, CancellationToken cancellationToken = default)
        {
            return atp.UpdateAccountHandleAsync(did, handle, cancellationToken);
        }


        /// <summary>
        /// Update the password for a user account as an administrator.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UpdateAccountPasswordAsync (FishyFlip.Models.ATDid did, string password, CancellationToken cancellationToken = default)
        {
            return atp.UpdateAccountPasswordAsync(did, password, cancellationToken);
        }


        /// <summary>
        /// Administrative action to update an account's signing key in their Did document.
        /// </summary>
        /// <param name="did"></param>
        /// <param name="signingKey">Did-key formatted public key</param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UpdateAccountSigningKeyAsync (FishyFlip.Models.ATDid did, FishyFlip.Models.ATDid signingKey, CancellationToken cancellationToken = default)
        {
            return atp.UpdateAccountSigningKeyAsync(did, signingKey, cancellationToken);
        }


        /// <summary>
        /// Update the service-specific admin status of a subject (account, record, or blob).
        /// </summary>
        /// <param name="subject">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoBlobRef"/> (com.atproto.admin.defs#repoBlobRef) <br/>
        /// </param>
        /// <param name="takedown"></param>
        /// <param name="deactivated"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Admin.UpdateSubjectStatusOutput?>> UpdateSubjectStatusAsync (ATObject subject, FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr? takedown = default, FishyFlip.Lexicon.Com.Atproto.Admin.StatusAttr? deactivated = default, CancellationToken cancellationToken = default)
        {
            return atp.UpdateSubjectStatusAsync(subject, takedown, deactivated, cancellationToken);
        }

    }
}

