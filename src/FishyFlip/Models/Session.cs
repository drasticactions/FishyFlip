// <copyright file="Session.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Session
{
    [JsonConstructor]
    public Session(
        ATDid did,
        ATHandler handle,
        string email,
        string accessJwt,
        string refreshJwt)
    {
        this.Did = did;
        this.Handle = handle;
        this.Email = email;
        this.AccessJwt = accessJwt;
        this.RefreshJwt = refreshJwt;
    }

    public ATDid Did { get; }

    public ATHandler Handle { get; }

    public string Email { get; }

    public string AccessJwt { get; }

    public string RefreshJwt { get; }
}
