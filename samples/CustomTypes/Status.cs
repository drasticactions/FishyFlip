// <copyright file="Status.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using FishyFlip.Lexicon;
using PeterO.Cbor;

namespace CustomTypes;

public partial class Status : ATObject
{

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="createdAt"></param>
    public Status(string? status, DateTime? createdAt = default)
    {
        this.StatusValue = status;
        this.CreatedAt = createdAt ?? DateTime.UtcNow;
        this.Type = "xyz.statusphere.status";
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    public Status()
    {
        this.Type = "xyz.statusphere.status";
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    public Status(CBORObject obj)
    {
        if (obj["status"] is not null) this.StatusValue = obj["status"].AsString();
        if (obj["createdAt"] is not null) this.CreatedAt = obj["createdAt"].ToDateTime();
    }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [JsonPropertyName("status")]
    public string? StatusValue { get; set; }

    /// <summary>
    /// Gets or sets the createdAt.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public const string RecordType = "xyz.statusphere.status";
}