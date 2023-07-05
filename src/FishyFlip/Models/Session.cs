// <copyright file="Session.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public class Session
{
    [JsonConstructor]
    public Session(
        AtUri did,
        AtUri handle,
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

    public AtUri Did { get; }

    public AtUri Handle { get; }

    public string Email { get; }

    public string AccessJwt { get; }

    public string RefreshJwt { get; }
}
