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
    static string handle;
    static string did;
    static string media_post;
    static string images_post;
    static string external_post;
    static string quote_post;
    static string quote_post_2;
    static string post_thread;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        did = (string?)context.Properties["BLUESKY_TEST_DID"] ?? throw new ArgumentNullException();
        images_post = (string?)context.Properties["BLUESKY_TEST_IMAGES_POST"] ?? throw new ArgumentNullException();
        media_post = (string?)context.Properties["BLUESKY_TEST_MEDIA_POST"] ?? throw new ArgumentNullException();
        images_post = (string?)context.Properties["BLUESKY_TEST_IMAGES_POST"] ?? throw new ArgumentNullException();
        external_post = (string?)context.Properties["BLUESKY_TEST_EXTERNAL_POST"] ?? throw new ArgumentNullException();
        post_thread = (string?)context.Properties["BLUESKY_TEST_POST_THREAD"] ?? throw new ArgumentNullException();
        quote_post = (string?)context.Properties["BLUESKY_TEST_QUOTE_POST"] ?? throw new ArgumentNullException();
        quote_post_2 = (string?)context.Properties["BLUESKY_TEST_QUOTE_POST_2"] ?? throw new ArgumentNullException();
        string instance = (string?)context.Properties["BLUESKY_INSTANCE_URL"] ?? throw new ArgumentNullException();
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
    [Ignore("Can't create MediaWithPostRecord. Not sure if that's broken in Bluesky.")]
    public async Task GetRecordWithMediaPostRecordTest()
    {
        var postUri = ATUri.Create(media_post);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Uri!.ToString());
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.RecordWithMedia, success.Value?.Embed.Type);
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

    private static void HandleProgressStatus(CarProgressStatusEvent e)
    {
        var cid = e.Cid;
        var bytes = e.Bytes;
        var test = CBORObject.DecodeFromBytes(bytes);
    }
}