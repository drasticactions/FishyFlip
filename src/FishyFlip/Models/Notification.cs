// <copyright file="Notification.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a notification.
/// </summary>
public record Notification(ATUri Uri, Cid Cid, string Reason, bool IsRead, DateTime IndexedAt, IReadOnlyList<Label> Labels, ATRecord? Record, FeedProfile Author);
