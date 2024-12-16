// <copyright file="PostView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Lexicon.App.Bsky.Feed;

/// <summary>
/// Post View.
/// </summary>
public partial class PostView : ATObject
{
    /// <summary>
    /// Gets the post record.
    /// This is a helper property for getting the Post record,
    /// which is stored in the Record property and listed as "unknown" in the Bluesky lexicon.
    /// </summary>
    [JsonIgnore]
    public Post? PostRecord => this.Record as Post;
}