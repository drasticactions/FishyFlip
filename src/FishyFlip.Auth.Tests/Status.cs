// <copyright file="Status.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using FishyFlip.Lexicon;
using PeterO.Cbor;

namespace FishyFlip.Tests;

/// <summary>
/// Status.
/// </summary>
public class Status : ATObject
{
    /// <summary>
    /// The Record Type.
    /// </summary>
    public const string RecordType = "xyz.statusphere.status";

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <param name="status">The Status.</param>
    /// <param name="createdAt">The created date.</param>
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
    /// <param name="obj">The CBORObject.</param>
    public Status(CBORObject obj)
    {
        if (obj["status"] is not null)
        {
            this.StatusValue = obj["status"].AsString();
        }

        if (obj["createdAt"] is not null)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
        }
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
}