// <copyright file="ReplyRef.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record Reply(ReplyRef Root, ReplyRef Parent);

public record ReplyRef(string Cid, AtUri Uri);