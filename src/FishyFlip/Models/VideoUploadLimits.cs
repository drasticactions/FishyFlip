// <copyright file="VideoUploadLimits.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the limits and status of video uploads for a user.
/// </summary>
/// <param name="CanUpload">Indicates whether the user can upload more videos.</param>
/// <param name="RemainingDailyVideos">The number of videos the user can still upload today.</param>
/// <param name="RemainingDailyBytes">The number of bytes the user can still upload today.</param>
/// <param name="Error">An error message if the user cannot upload videos.</param>
/// <param name="Message">A message providing additional information about the upload status.</param>
public record VideoUploadLimits(bool CanUpload, int RemainingDailyVideos, int RemainingDailyBytes, string Error, string Message);