// <copyright file="ResultExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Result Extensions.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Throw ATNetworkErrorException if Result is an ATError object..
    /// </summary>
    /// <typeparam name="T">Base object.</typeparam>
    /// <param name="result">Result object.</param>
    /// <returns>Result object if success.</returns>
    /// <exception cref="ATNetworkErrorException">Thrown if the result is an atError object.</exception>
    public static T HandleResult<T>(this Result<T> result)
    {
        if (result.IsT1)
        {
            throw new ATNetworkErrorException(result.AsT1);
        }

        return result.AsT0;
    }
}
