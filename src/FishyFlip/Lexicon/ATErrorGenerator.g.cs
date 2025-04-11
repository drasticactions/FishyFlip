// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon
{
    /// <summary>
    /// Generates casted ATError messages if available.
    /// </summary>
    public static class ATErrorGenerator
    {
        public static FishyFlip.Models.ATError Generate(int statusCode, ErrorDetail detail)
        {
            switch (detail.Error)
            {
                case "MemberAlreadyExists":
                    return new MemberAlreadyExistsError(statusCode, detail);
                case "ReactionMessageDeleted":
                    return new ReactionMessageDeletedError(statusCode, detail);
                case "ReactionLimitReached":
                    return new ReactionLimitReachedError(statusCode, detail);
                case "ReactionInvalidValue":
                    return new ReactionInvalidValueError(statusCode, detail);
                case "InvalidSwap":
                    return new InvalidSwapError(statusCode, detail);
                case "AccountNotFound":
                    return new AccountNotFoundError(statusCode, detail);
                case "ExpiredToken":
                    return new ExpiredTokenError(statusCode, detail);
                case "InvalidToken":
                    return new InvalidTokenError(statusCode, detail);
                case "InvalidEmail":
                    return new InvalidEmailError(statusCode, detail);
                case "InvalidHandle":
                    return new InvalidHandleError(statusCode, detail);
                case "InvalidPassword":
                    return new InvalidPasswordError(statusCode, detail);
                case "InvalidInviteCode":
                    return new InvalidInviteCodeError(statusCode, detail);
                case "HandleNotAvailable":
                    return new HandleNotAvailableError(statusCode, detail);
                case "UnsupportedDomain":
                    return new UnsupportedDomainError(statusCode, detail);
                case "UnresolvableDid":
                    return new UnresolvableDidError(statusCode, detail);
                case "IncompatibleDidDoc":
                    return new IncompatibleDidDocError(statusCode, detail);
                case "AccountTakedown":
                    return new AccountTakedownError(statusCode, detail);
                case "AuthFactorTokenRequired":
                    return new AuthFactorTokenRequiredError(statusCode, detail);
                case "DuplicateTemplateName":
                    return new DuplicateTemplateNameError(statusCode, detail);
                case "MemberNotFound":
                    return new MemberNotFoundError(statusCode, detail);
                case "CannotDeleteSelf":
                    return new CannotDeleteSelfError(statusCode, detail);
                case "SetNotFound":
                    return new SetNotFoundError(statusCode, detail);
                case "SubjectHasAction":
                    return new SubjectHasActionError(statusCode, detail);
                case "DuplicateCreate":
                    return new DuplicateCreateError(statusCode, detail);
                case "BlockedActor":
                    return new BlockedActorError(statusCode, detail);
                case "BlockedByActor":
                    return new BlockedByActorError(statusCode, detail);
                case "BlobNotFound":
                    return new BlobNotFoundError(statusCode, detail);
                case "RepoNotFound":
                    return new RepoNotFoundError(statusCode, detail);
                case "RepoTakendown":
                    return new RepoTakendownError(statusCode, detail);
                case "RepoSuspended":
                    return new RepoSuspendedError(statusCode, detail);
                case "RepoDeactivated":
                    return new RepoDeactivatedError(statusCode, detail);
                case "BlockNotFound":
                    return new BlockNotFoundError(statusCode, detail);
                case "NotFound":
                    return new NotFoundError(statusCode, detail);
                case "UnknownFeed":
                    return new UnknownFeedError(statusCode, detail);
                case "HostNotFound":
                    return new HostNotFoundError(statusCode, detail);
                case "UnknownList":
                    return new UnknownListError(statusCode, detail);
                case "RecordNotFound":
                    return new RecordNotFoundError(statusCode, detail);
                case "ActorNotFound":
                    return new ActorNotFoundError(statusCode, detail);
                case "BadExpiration":
                    return new BadExpirationError(statusCode, detail);
                case "HandleNotFound":
                    return new HandleNotFoundError(statusCode, detail);
                case "DidNotFound":
                    return new DidNotFoundError(statusCode, detail);
                case "DidDeactivated":
                    return new DidDeactivatedError(statusCode, detail);
                case "HostBanned":
                    return new HostBannedError(statusCode, detail);
                case "EmojiNotFound":
                    return new EmojiNotFoundError(statusCode, detail);
                case "DestinationExists":
                    return new DestinationExistsError(statusCode, detail);
                case "BadQueryString":
                    return new BadQueryStringError(statusCode, detail);
                case "FutureCursor":
                    return new FutureCursorError(statusCode, detail);
                case "ConsumerTooSlow":
                    return new ConsumerTooSlowError(statusCode, detail);
                case "TokenRequired":
                    return new TokenRequiredError(statusCode, detail);
                default:
                    return new FishyFlip.Models.ATError(statusCode, detail);
            }
        }
    }
}

