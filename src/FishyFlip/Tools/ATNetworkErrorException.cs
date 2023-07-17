// <copyright file="ATNetworkErrorException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// AT Network Error Exception.
/// Thrown if given a failed result from ATProtocol.
/// </summary>
public class ATNetworkErrorException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATNetworkErrorException"/> class.
    /// </summary>
    /// <param name="error">Error message.</param>
    public ATNetworkErrorException(Error error)
        : base($"{error.StatusCode}: {error.Detail}")
    {
        this.Error = error;
    }

    /// <summary>
    /// Gets the base error.
    /// </summary>
    public Error Error { get; }
}
