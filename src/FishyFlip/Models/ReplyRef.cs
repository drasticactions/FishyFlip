// <copyright file="ReplyRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a reference to a reply.
/// </summary>
public record ReplyRef(ATCid Cid, ATUri Uri);