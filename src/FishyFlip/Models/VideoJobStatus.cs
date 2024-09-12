// <copyright file="VideoJobStatus.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the status of a job.
/// </summary>
/// <param name="JobId">The unique identifier for the job.</param>
/// <param name="Did">The ATDid associated with the job.</param>
/// <param name="State">The current state of the job.</param>
/// <param name="Progress">The progress of the job, represented as an integer percentage (default is 0).</param>
/// <param name="Blob">An optional Blob associated with the job.</param>
/// <param name="Error">An optional error message if the job encountered an error.</param>
/// <param name="Message">An optional message providing additional information about the job.</param>
public record VideoJobStatus(string JobId, ATDid Did, string State, int Progress = 0, Blob? Blob = default, string? Error = null, string? Message = null);