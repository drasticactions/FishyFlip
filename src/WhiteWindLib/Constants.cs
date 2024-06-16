// <copyright file="Constants.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace WhiteWindLib;

#pragma warning disable SA1600 // Elements should be documented
public static class Constants
{
    public static class Urls
    {
        public static class WhiteWind
        {
            public const string GetAuthorPosts = "/xrpc/com.whtwnd.blog.getAuthorPosts";
            public const string GetEntryMetadataByName = "/xrpc/com.whtwnd.blog.getEntryMetadataByName";
            public const string GetMentionsByEntry = "/xrpc/com.whtwnd.blog.getMentionsByEntry";
            public const string NotifyOfNewEntry = "/xrpc/com.whtwnd.blog.notifyOfNewEntry";
        }
    }

    public static class WhiteWindTypes
    {
        public const string Entry = "com.whtwnd.blog.entry";
        public const string Comment = "com.whtwnd.blog.comment";
        public const string MainComment = "com.whtwnd.blog.comment#main";
        public const string Mentions = "com.whtwnd.blog.mentions";
    }

    public static class WhiteWindVisibility
    {
        public const string Public = "public";
        public const string Unlisted = "unlisted";
        public const string Private = "private";
    }
}
#pragma warning restore SA1600 // Elements should be documented