// <copyright file="RefreshToken.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;

namespace FishyFlip.Commands;

public record RefreshToken(string AccessJwt, string RefreshJwt, string Handle, AtUri Did)
{
}
