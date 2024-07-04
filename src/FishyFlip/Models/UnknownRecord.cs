// <copyright file="UnknownRecord.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents an unknown record in the system.
/// This type has not been set yet.
/// Inherits from the ATRecord base class.
/// </summary>
public class UnknownRecord : ATRecord
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownRecord"/> class.
    /// </summary>
    /// <param name="type">The type of the unknown record.</param>
    /// <param name="json">The json of the unknown record.</param>
    public UnknownRecord(string type, string json)
        : base(type)
    {
        this.Json = json;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownRecord"/> class.
    /// </summary>
    /// <param name="type">The type of the unknown record.</param>
    [JsonConstructor]
    public UnknownRecord()
    {
        this.Json = string.Empty;
    }

    /// <summary>
    /// Gets the json of the unknown record.
    /// </summary>
    public string Json { get; }
}