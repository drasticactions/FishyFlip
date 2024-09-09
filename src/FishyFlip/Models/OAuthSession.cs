// <copyright file="OAuthSession.cs" company="Drastic Actions">
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
public class OAuthSession
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuthSession"/> class.
    /// </summary>
    /// <param name="session">The session information.</param>
    /// <param name="proofKey">The proof key.</param>
    [JsonConstructor]
    public OAuthSession(Session session, string proofKey)
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
    /// Converts a JSON string to an OAuthSession.
    /// </summary>
    /// <param name="json">Json.</param>
    /// <returns>OAuthSession.</returns>
    public static OAuthSession? FromString(string json)
    {
        return JsonSerializer.Deserialize<OAuthSession>(json, SourceGenerationContext.Default.OAuthSession);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize<OAuthSession>(this, SourceGenerationContext.Default.OAuthSession);
    }
}