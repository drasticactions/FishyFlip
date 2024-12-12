// <copyright file="ATProtocolUnknownError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// AT Network ATError Exception.
/// Thrown if given a failed result from ATProtocol.
/// </summary>
public class ATProtocolUnknownError : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATProtocolUnknownError"/> class.
    /// </summary>
    /// <param name="atError">ATError message.</param>
    public ATProtocolUnknownError(string atError)
            : base(atError)
    {
    }
}