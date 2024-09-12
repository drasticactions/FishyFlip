// <copyright file="UploadVideoResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents the response received after uploading a video.
/// </summary>
/// <param name="JobStatus">The status of the video job.</param>
public record UploadVideoResponse(VideoJobStatus JobStatus);