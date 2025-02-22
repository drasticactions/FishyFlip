// <copyright file="DataAdd.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents a data addition with a key and a CID.
/// </summary>
public record DataAdd([property: JsonPropertyName("key")] string Key, [property: JsonPropertyName("cid")] Cid Cid);