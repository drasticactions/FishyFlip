// <copyright file="ResultExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Result Extensions.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Throw ATNetworkErrorException if Result is an ATError object.
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

    /// <summary>
    /// Deconstructs the Result object into its value and error components.
    /// </summary>
    /// <typeparam name="T">The type of the value component.</typeparam>
    /// <param name="result">The Result object to deconstruct.</param>
    /// <param name="value">The value component of the Result object.</param>
    /// <param name="error">The error component of the Result object.</param>
    public static void Deconstruct<T>(this Result<T> result, out T? value, out ATError? error)
    {
        value = result.IsT0 ? result.AsT0 : default;
        error = result.IsT1 ? result.AsT1 : default;
    }
}
