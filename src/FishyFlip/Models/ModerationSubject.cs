// <copyright file="ModerationSubject.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public class ModerationSubject
{
    [JsonConstructor]
    public ModerationSubject(string type, string? uri, Cid? cid, ATDid? did)
    {
        this.Uri = uri;
        this.Type = type;
        this.Cid = cid;
        this.Did = did;
    }

    public ATDid? Did { get; }

    public string? Uri { get; }

    public Cid? Cid { get; }

    [JsonPropertyName("$type")]
    public string Type { get; }
}

public class ModerationRecord
{
    [JsonConstructor]
    public ModerationRecord(ModerationSubject? subject, DateTime? createdAt, int id, string? reason, ATDid? reportedBy)
    {
        this.Id = id;
        this.Reason = reason;
        this.ReportedBy = reportedBy;
        this.Subject = subject;
        this.CreatedAt = createdAt ?? DateTime.Now;
    }

    public int Id { get; }

    public string? Reason { get; }

    public ATDid? ReportedBy { get; }

    public ModerationSubject? Subject { get; }

    public DateTime? CreatedAt { get; }
}
