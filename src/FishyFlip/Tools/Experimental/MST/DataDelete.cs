// <copyright file="DataDelete.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// Represents a data deletion with a key and a CID.
/// </summary>
public record DataDelete([property: JsonPropertyName("key")] string Key, [property: JsonPropertyName("cid")] Cid Cid);
