// <copyright file="ExceptionExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools
{
    /// <summary>
    /// Provides extension methods for exceptions.
    /// </summary>
    internal static class ExceptionExtensions
    {
        /// <summary>
        /// Throws an ArgumentNullException if the provided object is null.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="t">The object to check for nullity.</param>
        /// <returns>The same object if it is not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided object is null.</exception>
        internal static T ThrowIfNull<T>(this T? t)
        {
            ArgumentNullException.ThrowIfNull(t);
            return t;
        }
    }
}