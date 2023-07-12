// <copyright file="External.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class External
{
    [JsonConstructor]
    public External(Image? thumb, string? title, string? description, string? uri)
    {
        this.Thumb = thumb;
        this.Title = title;
        this.Description = description;
        this.Uri = uri;
    }

    public External(CBORObject obj)
    {
        if (obj["thumb"] is not null)
        {
            this.Thumb = new Image(obj["thumb"]);
        }

        this.Uri = obj["uri"].AsString();
        this.Title = obj["title"].AsString();
        this.Description = obj["description"].AsString();
    }

    public Image? Thumb { get; }

    public string? Title { get; }

    public string? Description { get; }

    public string? Uri { get; }
}