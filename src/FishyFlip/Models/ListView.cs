// <copyright file="ListView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a ListView object.
/// </summary>
/// <param name="Uri">The URI of the ListView.</param>
/// <param name="ATCid">The CID of the ListView.</param>
/// <param name="Name">The name of the ListView.</param>
/// <param name="Purpose">The purpose of the ListView.</param>
/// <param name="Description">The description of the ListView.</param>
/// <param name="Creator">The profile of the creator of the ListView.</param>
/// <param name="Viewer">The viewer of the ListView.</param>
/// <param name="IndexedAt">The date and time when the ListView was indexed.</param>
public record ListView(ATUri Uri, ATCid Cid, string Name, string Purpose, string Description, ActorProfile Creator, Viewer Viewer, DateTime IndexedAt);
