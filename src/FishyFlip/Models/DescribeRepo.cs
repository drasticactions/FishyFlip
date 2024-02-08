// <copyright file="DescribeRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a description of a repository.
/// </summary>
public class DescribeRepo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DescribeRepo"/> class.
    /// </summary>
    /// <param name="handle">The handle of the repository.</param>
    /// <param name="did">The ATDid of the repository.</param>
    /// <param name="didDoc">The DidDoc of the repository.</param>
    /// <param name="collections">The collections of the repository.</param>
    /// <param name="handleIsCorrect">A value indicating whether the handle is correct.</param>
    [JsonConstructor]
    public DescribeRepo(string handle, ATDid did, DidDoc didDoc, List<object> collections, bool handleIsCorrect)
    {
        this.Handle = handle;
        this.Did = did;
        this.DidDoc = didDoc;
        this.Collections = collections;
        this.HandleIsCorrect = handleIsCorrect;
    }

    /// <summary>
    /// Gets the handle of the repository.
    /// </summary>
    public string Handle { get; }

    /// <summary>
    /// Gets the ATDid of the repository.
    /// </summary>
    public ATDid Did { get; }

    /// <summary>
    /// Gets the DidDoc of the repository.
    /// </summary>
    public DidDoc DidDoc { get; }

    /// <summary>
    /// Gets the collections of the repository.
    /// </summary>
    public List<object> Collections { get; }

    /// <summary>
    /// Gets a value indicating whether the handle is correct.
    /// </summary>
    public bool HandleIsCorrect { get; }
}
