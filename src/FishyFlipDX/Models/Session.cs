// <copyright file="Session.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a session object that contains information about a user session.
/// </summary>
public class Session
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Session"/> class.
    /// </summary>
    /// <param name="did">The ATDid associated with the session.</param>
    /// <param name="didDoc">The DidDoc associated with the session.</param>
    /// <param name="handle">The ATHandle associated with the session.</param>
    /// <param name="email">The email associated with the session.</param>
    /// <param name="accessJwt">The access JWT associated with the session.</param>
    /// <param name="refreshJwt">The refresh JWT associated with the session.</param>
    [JsonConstructor]
    public Session(
        ATDid did,
        DidDoc? didDoc,
        ATHandle handle,
        string? email,
        string accessJwt,
        string refreshJwt)
    {
        this.Did = did;
        this.DidDoc = didDoc;
        this.Handle = handle;
        this.Email = email;
        this.AccessJwt = accessJwt;
        this.RefreshJwt = refreshJwt;
    }

    /// <summary>
    /// Gets the ATDid associated with the session.
    /// </summary>
    public ATDid Did { get; }

    /// <summary>
    /// Gets the DidDoc associated with the session.
    /// </summary>
    public DidDoc? DidDoc { get; }

    /// <summary>
    /// Gets the ATHandle associated with the session.
    /// </summary>
    public ATHandle Handle { get; }

    /// <summary>
    /// Gets the email associated with the session.
    /// </summary>
    public string? Email { get; }

    /// <summary>
    /// Gets the access JWT associated with the session.
    /// </summary>
    public string AccessJwt { get; }

    /// <summary>
    /// Gets the refresh JWT associated with the session.
    /// </summary>
    public string RefreshJwt { get; }
}