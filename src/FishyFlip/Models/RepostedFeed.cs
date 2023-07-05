// <copyright file="RepostedFeed.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record RepostedFeed(Profile[] RepostedBy, string? Cursor);