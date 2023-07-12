// <copyright file="Label.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record Label(ATUri Src, string Uri, Cid Cid, string Val, bool Neg, DateTime Cts);