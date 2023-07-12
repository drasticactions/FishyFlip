// <copyright file="FrameInfo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Frame Info.
/// </summary>
public class FrameInfo
{
    public FrameInfo(CBORObject obj)
    {
        this.Name = obj["name"]?.AsString();
        this.Message = obj["message"]?.AsString();
    }

    public string? Name { get; }

    public string? Message { get; }
}