// <copyright file="RepoList.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public record RepoList(RepoRef[] Repos, string? Cursor);