// <copyright file="IATIdentifierResolver.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Interface for resolving AT identifiers.
/// </summary>
public interface IATIdentifierResolver
{
    /// <summary>
    /// Resolves the identifier to an <see cref="ATDid"/> for the given AT.
    /// </summary>
    /// <param name="identifier">
    /// The <see cref="ATIdentifier"/> to resolve for.
    /// If the value is a <see cref="ATDid"/>, it will be returned as is.
    /// </param>
    /// <returns>The resolved identifier.</returns>
    Task<Result<ATDid?>> ToATDidAsync(ATIdentifier identifier);

    /// <summary>
    /// Resolves the identifier to an <see cref="ATHandle"/> for the given AT.
    /// </summary>
    /// <param name="identifier">
    /// The <see cref="ATIdentifier"/> to resolve for.
    /// If the value is a <see cref="ATHandle"/>, it will be returned as is.
    /// </param>
    /// <returns>The resolved identifier.</returns>
    Task<Result<ATHandle?>> ToATHandleAsync(ATIdentifier identifier);
}