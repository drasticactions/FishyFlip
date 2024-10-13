// <copyright file="AnonymousTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using FishyFlip.Tools;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FishyFlip.Tools.CarDecoder;

namespace FishyFlip.Tests;

[TestClass]
public class AnonymousTests
{
    static ATProtocol proto;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        string instance = "https://bsky.social";
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
    }

    [TestMethod]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q4gqbk2y", "")]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxcq7bwwo2j", Constants.EmbedTypes.Record)]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46sr63j7r2m", Constants.EmbedTypes.External)]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q57gcs2k", Constants.EmbedTypes.Images)]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46xtosyvf2y", Constants.EmbedTypes.Video)]
    public async Task TestPostAsync(string atUri, string embedType)
    {
        var postUri = ATUri.Create(atUri);
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Uri!.ToString());
                Assert.IsNotNull(success.Value);
                Assert.AreEqual(success.Value.Type, Constants.FeedType.Post);

                if (!string.IsNullOrEmpty(embedType))
                {
                    Assert.IsNotNull(success.Value.Embed);
                    Assert.AreEqual(success.Value.Embed.Type, embedType);
                    switch (success.Value.Embed.Type)
                    {
                        case Constants.EmbedTypes.Record:
                            var recordEmbed = (RecordEmbed)success.Value.Embed;
                            Assert.IsNotNull(recordEmbed);
                            Assert.IsNotNull(recordEmbed.Record);
                            Assert.IsNotNull(recordEmbed.Record.Cid);
                            Assert.IsNotNull(recordEmbed.Record.Uri);
                            break;
                        case Constants.EmbedTypes.External:
                            var externalEmbed = (ExternalEmbed)success.Value.Embed;
                            Assert.IsNotNull(externalEmbed);
                            Assert.IsNotNull(externalEmbed.External);
                            var external = externalEmbed.External;
                            Assert.IsTrue(!string.IsNullOrEmpty(external.Description));
                            Assert.IsTrue(!string.IsNullOrEmpty(external.Title));
                            Assert.IsTrue(!string.IsNullOrEmpty(external.Uri));
                            Assert.IsNotNull(external.Thumb);
                            Assert.IsTrue(!string.IsNullOrEmpty(external.Thumb.MimeType));
                            Assert.IsTrue(!string.IsNullOrEmpty(external.Thumb.Type));
                            Assert.IsNotNull(external.Thumb.Ref);
                            Assert.IsNotNull(external.Thumb.Ref.Link);
                            break;
                        case Constants.EmbedTypes.Images:
                            var imagesEmbed = (ImagesEmbed)success.Value.Embed;
                            Assert.IsNotNull(imagesEmbed);
                            Assert.IsNotNull(imagesEmbed.Images);
                            foreach (var image in imagesEmbed.Images)
                            {
                                Assert.IsNotNull(image);
                                image.Image.ThrowIfNull();
                                image.Image?.Ref.ThrowIfNull();
                                Assert.IsTrue(!string.IsNullOrEmpty(image.Image?.MimeType));
                            }

                            break;
                        case Constants.EmbedTypes.Video:
                            var videoEmbed = (VideoEmbed)success.Value.Embed;
                            Assert.IsNotNull(videoEmbed);
                            Assert.IsNotNull(videoEmbed.Video);
                            videoEmbed.Video?.Ref.ThrowIfNull();
                            Assert.IsTrue(!string.IsNullOrEmpty(videoEmbed.Video?.MimeType));
                            Assert.IsTrue(!string.IsNullOrEmpty(videoEmbed.Video?.Type));
                            Assert.IsNotNull(videoEmbed.AspectRatio);
                            Assert.IsTrue(videoEmbed.AspectRatio.Width > 0);
                            Assert.IsTrue(videoEmbed.AspectRatio.Height > 0);
                            break;
                        default:
                            Assert.Fail("Type not listed for test.");
                            break;
                    }
                }
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    [DataRow("did:plc:okblbaji7rz243bluudjlgxt")]
    public async Task DescribeRepoTest(string did)
    {
        var repo = ATDid.Create(did);
        var describe = (await AnonymousTests.proto.Repo.DescribeRepoAsync(repo)).HandleResult();
        Assert.IsTrue(describe is not null);
        Assert.IsTrue(describe.HandleIsCorrect);
        Assert.IsTrue(describe.Did is not null);
        Assert.AreEqual(describe.Did!.ToString(), repo.ToString());
    }
}