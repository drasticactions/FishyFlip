// <copyright file="AuthSession.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Models;

/// <summary>
/// Represents an OAuth session.
/// </summary>
/// <param name="Session">The session information.</param>
/// <param name="ProofKey">The proof key.</param>
public class AuthSession
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthSession"/> class.
    /// </summary>
    /// <param name="session">The session information.</param>
    /// <param name="proofKey">The proof key.</param>
    [JsonConstructor]
    public AuthSession(Session session, string proofKey = "")
    {
        this.Session = session;
        this.ProofKey = proofKey;
    }

    /// <summary>
    /// Gets the session information.
    /// </summary>
    public Session Session { get; }

    /// <summary>
    /// Gets the proof key.
    /// </summary>
    public string ProofKey { get; }

    /// <summary>
    /// Converts a JSON string to an AuthSession.
    /// </summary>
    /// <param name="json">Json.</param>
    /// <returns>OAuthSession.</returns>
    public static AuthSession? FromString(string json)
    {
        return JsonSerializer.Deserialize<AuthSession>(json, SourceGenerationContext.Default.AuthSession);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize<AuthSession>(this, SourceGenerationContext.Default.AuthSession);
    }
}