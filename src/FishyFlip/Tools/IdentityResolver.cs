// <copyright file="IdentityResolver.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Uses <see cref="FishyFlip.Lexicon.Com.Atproto.Identity.ATProtoIdentity"/> to resolve AT identifiers.
/// </summary>
public class IdentityResolver : IATIdentifierResolver
{
    private readonly ATProtocol atp;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityResolver"/> class.
    /// </summary>
    /// <param name="atp"><see cref="ATProtocol"/>.</param>
    public IdentityResolver(ATProtocol atp)
    {
        this.atp = atp;
    }

    /// <inheritdoc/>
    public async Task<Result<ATDid?>> ToATDidAsync(ATIdentifier identifier)
    {
        if (identifier is ATDid atDid)
        {
            return atDid;
        }

        if (identifier is ATHandle atHandle)
        {
            var (result, error) = await this.atp.Identity.ResolveHandleAsync(atHandle);
            if (error != null)
            {
                return error;
            }

            return result?.Did;
        }

        throw new NotImplementedException($"Resolving {identifier.GetType()} is not implemented.");
    }

    /// <inheritdoc/>
    public async Task<Result<ATHandle?>> ToATHandleAsync(ATIdentifier identifier)
    {
        if (identifier is ATHandle atHandle)
        {
            return atHandle;
        }

        if (identifier is ATDid atDid)
        {
            var (result, error) = await this.atp.Identity.ResolveDidAsync(atDid);
            if (error != null)
            {
                return error;
            }

            return result?.DidDoc?.GetHandle();
        }

        throw new NotImplementedException($"Resolving {identifier.GetType()} is not implemented.");
    }
}