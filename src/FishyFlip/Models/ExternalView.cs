// <copyright file="ExternalView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an external view of a resource.
/// </summary>
public record ExternalView(string Thumb, string Title, string Uri, string Description);