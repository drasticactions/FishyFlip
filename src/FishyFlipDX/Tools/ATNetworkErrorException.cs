// <copyright file="ATNetworkErrorException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// AT Network ATError Exception.
/// Thrown if given a failed result from ATProtocol.
/// </summary>
public class ATNetworkErrorException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATNetworkErrorException"/> class.
    /// </summary>
    /// <param name="atError">ATError message.</param>
    public ATNetworkErrorException(ATError atError)
        : base($"{atError.StatusCode}: {atError.Detail}")
    {
        this.AtError = atError;
    }

    /// <summary>
    /// Gets the base atError.
    /// </summary>
    public ATError AtError { get; }
}
