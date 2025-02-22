// <copyright file="DataUpdate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools.Experimental.MST;

/// <summary>
/// DataUpdate class.
/// </summary>
public record DataUpdate([property: JsonPropertyName("key")] string Key, [property: JsonPropertyName("prev")] Cid Prev, [property: JsonPropertyName("cid")] Cid Cid);