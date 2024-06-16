// <copyright file="Label.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Metadata tag on an atproto resource (eg, repo or record).
/// </summary>
/// <param name="Src">DID of the actor who created this label.</param>
/// <param name="Uri">AT URI of the record, repository (account), or other resource that this label applies to.</param>
/// <param name="ATCid">Optionally, CID specifying the specific version of 'uri' resource this label applies to.</param>
/// <param name="Val">The short string name of the value or type of this label.</param>
/// <param name="Neg">If true, this is a negation label, overwriting a previous label.</param>
/// <param name="Cts">Timestamp when this label was created.</param>
/// <param name="Ver">Version of the label.</param>
public record Label(ATUri Src, string Uri, ATCid Cid, string Val, bool Neg, DateTime Cts, int Ver);