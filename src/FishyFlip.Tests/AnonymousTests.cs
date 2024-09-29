// <copyright file="AnonymousTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FishyFlip.Tools.CarDecoder;

namespace FishyFlip.Tests;

[TestClass]
public class AnonymousTests
{
    static ATProtocol proto;
    static string did;
    static string media_post;
    static string images_post;
    static string external_post;
    static string quote_post;
    static string post_thread;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // drasticactions.xn--q9jyb4c
        did = "did:plc:okblbaji7rz243bluudjlgxt";
        images_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q57gcs2k";
        media_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46xtosyvf2y";
        external_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46sr63j7r2m";
        post_thread = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q4gqbk2y";
        quote_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxcq7bwwo2j";
        string instance = "https://bsky.social";
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
    }

    [TestMethod]
    public async Task GetPostRecordTest()
    {
        var postUri = ATUri.Create(post_thread);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Uri!.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetQuotePostRecordTest()
    {
        var postUri = ATUri.Create(quote_post);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString().ToString(), success!.Uri!.ToString());
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.Record, success.Value?.Embed?.Type);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetExternalPostRecordTest()
    {
        var postUri = ATUri.Create(external_post);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Uri!.ToString());
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.External, success.Value?.Embed?.Type);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetImagesPostRecordTest()
    {
        var postUri = ATUri.Create(images_post);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.Images, success.Value?.Embed.Type);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetRecordWithVideoPostRecordTest()
    {
        var postUri = ATUri.Create(media_post);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Uri!.ToString());
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.Video, success.Value?.Embed.Type);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task DescribeRepoTest()
    {
        var repo = ATDid.Create(did);
        var describe = (await AnonymousTests.proto.Repo.DescribeRepoAsync(repo)).HandleResult();
        Assert.IsTrue(describe is not null);
        Assert.IsTrue(describe.HandleIsCorrect);
        Assert.IsTrue(describe.Did is not null);
        Assert.AreEqual(describe.Did!.ToString(), repo.ToString());
    }
}