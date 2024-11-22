// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Com.Atproto.Identity
{

    /// <summary>
    /// com.atproto.identity Endpoint Class.
    /// </summary>
    public sealed class ATProtoIdentity
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ATProtoIdentity"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ATProtoIdentity(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Describe the credentials that should be included in the DID doc of an account that is migrating to this service.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Identity.GetRecommendedDidCredentialsOutput?>> GetRecommendedDidCredentialsAsync (CancellationToken cancellationToken = default)
        {
            return atp.GetRecommendedDidCredentialsAsync(cancellationToken);
        }


        /// <summary>
        /// Request an email with a code to in order to request a signed PLC operation. Requires Auth.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> RequestPlcOperationSignatureAsync (CancellationToken cancellationToken = default)
        {
            return atp.RequestPlcOperationSignatureAsync(cancellationToken);
        }


        /// <summary>
        /// Resolves a handle (domain name) to a DID.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Identity.ResolveHandleOutput?>> ResolveHandleAsync (FishyFlip.Models.ATHandle handle, CancellationToken cancellationToken = default)
        {
            return atp.ResolveHandleAsync(handle, cancellationToken);
        }


        /// <summary>
        /// Signs a PLC operation to update some value(s) in the requesting DID's document.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="rotationKeys"></param>
        /// <param name="alsoKnownAs"></param>
        /// <param name="verificationMethods"></param>
        /// <param name="services"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<FishyFlip.Lexicon.Com.Atproto.Identity.SignPlcOperationOutput?>> SignPlcOperationAsync (string? token = default, List<string>? rotationKeys = default, List<string>? alsoKnownAs = default, ATObject? verificationMethods = default, ATObject? services = default, CancellationToken cancellationToken = default)
        {
            return atp.SignPlcOperationAsync(token, rotationKeys, alsoKnownAs, verificationMethods, services, cancellationToken);
        }


        /// <summary>
        /// Validates a PLC operation to ensure that it doesn't violate a service's constraints or get the identity into a bad state, then submits it to the PLC registry
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> SubmitPlcOperationAsync (ATObject operation, CancellationToken cancellationToken = default)
        {
            return atp.SubmitPlcOperationAsync(operation, cancellationToken);
        }


        /// <summary>
        /// Updates the current account's handle. Verifies handle validity, and updates did:plc document if necessary. Implemented by PDS, and requires auth.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        public Task<Result<Success?>> UpdateHandleAsync (FishyFlip.Models.ATHandle handle, CancellationToken cancellationToken = default)
        {
            return atp.UpdateHandleAsync(handle, cancellationToken);
        }

    }
}

