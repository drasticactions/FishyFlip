// <copyright file="IJsonEncodable.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Interface for JSON encodable objects.
/// </summary>
/// <typeparam name="T">Object.</typeparam>
public interface IJsonEncodable<out T>
{
    /// <summary>
    /// Converts the object to a JSON string.
    /// </summary>
    /// <returns>JSON String.</returns>
    string ToJson();

    /// <summary>
    /// Converts the object to a UTF8 JSON Byte Array.
    /// </summary>
    /// <returns>JSON String.</returns>
    byte[] ToUtf8Json();

#if NET
    /// <summary>
    /// Converts a JSON object to the specified type.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <returns>From JSON Object.</returns>
    static abstract T FromJson(string obj);
#endif
}