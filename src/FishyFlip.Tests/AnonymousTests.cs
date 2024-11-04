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

    [TestMethod]
    public void MarkdownPostTest()
    {
        var markdownPost = @"Markdown Test: [FishyFlip](https://drasticactions.github.io/FishyFlip), #FishyFlip, [@drasticactions.dev](did:plc:yhgc5rlqhoezrx6fbawajxlh)";
        var post = MarkdownPost.Parse(markdownPost);
        Assert.IsTrue(post.OriginalMarkdown == markdownPost);
        Assert.IsTrue(post.Post == "Markdown Test: FishyFlip, #FishyFlip, @drasticactions.dev");
        Assert.IsTrue(post.Facets.Length == 3);
        Assert.IsTrue(post.Facets[0].Features![0]!.Type == Constants.FacetTypes.Link);
        Assert.IsTrue(post.Facets[0].Features![0]!.Uri == "https://drasticactions.github.io/FishyFlip");
        Assert.IsTrue(post.Facets[0].Index!.ByteStart == 15);
        Assert.IsTrue(post.Facets[0].Index!.ByteEnd == 24);
        Assert.IsTrue(post.Facets[1].Features![0]!.Type == Constants.FacetTypes.Mention);
        Assert.IsTrue(post.Facets[1].Features![0]!.Did!.ToString() == "did:plc:yhgc5rlqhoezrx6fbawajxlh");
        Assert.IsTrue(post.Facets[1].Index!.ByteStart == 38);
        Assert.IsTrue(post.Facets[1].Index!.ByteEnd == 57);
        Assert.IsTrue(post.Facets[2].Features![0]!.Type == Constants.FacetTypes.Tag);
        Assert.IsTrue(post.Facets[2].Features![0]!.Tag == "FishyFlip");
        Assert.IsTrue(post.Facets[2].Index!.ByteStart == 26);
        Assert.IsTrue(post.Facets[2].Index!.ByteEnd == 36);
    }

    [TestMethod]
    public async Task ValidateFacetHelpers()
    {
        var daDev = new FacetActorIdentifier(ATHandle.Create("drasticactions.dev")!, ATDid.Create("did:plc:okblbaji7rz243bluudjlgxt")!);
        var daJp = new FacetActorIdentifier(ATHandle.Create("drasticactions.jp")!, ATDid.Create("did:plc:okblbaji7rz243bluudjl2bt")!);

        var postText = "@drasticactions.dev This is a #test #test of #testing the #FishyFlip #API. https://github.com/drasticactions DAHome. @drasticactions.jp https://github.com/drasticactions/FishyFlip @drasticactions.dev Weee!";
        var postHandles = ATHandle.FromPostText(postText);
        Assert.IsTrue(postHandles.Length == 2);
        Assert.IsTrue(postHandles[0].Handle == "drasticactions.dev");
        Assert.IsTrue(postHandles[1].Handle == "drasticactions.jp");

        var handleFacets = Facet.ForMentions(postText, new FacetActorIdentifier[] { daDev, daJp });

        Assert.IsTrue(handleFacets.Length == 3);
        Assert.IsTrue(handleFacets[0].Index!.ByteStart == 0);
        Assert.IsTrue(handleFacets[0].Index!.ByteEnd == 19);
        Assert.IsTrue(handleFacets[0].Features![0]!.Did! == daDev.Did);

        Assert.IsTrue(handleFacets[1].Index!.ByteStart == 117);
        Assert.IsTrue(handleFacets[1].Index!.ByteEnd == 135);
        Assert.IsTrue(handleFacets[1].Features![0]!.Did! == daJp.Did);

        Assert.IsTrue(handleFacets[2].Features![0]!.Did! == daDev.Did);
        Assert.IsTrue(handleFacets[2].Index!.ByteStart == 180);
        Assert.IsTrue(handleFacets[2].Index!.ByteEnd == 199);

        var hashtagFacets = Facet.ForHashtags(postText);
        Assert.IsTrue(hashtagFacets.Length == 5);
        var uriFacets = Facet.ForUris(postText);
        Assert.IsTrue(uriFacets.Length == 2);
        var baseUriFacets = Facet.ForUris(postText, "DAHome", "https://github.com/drasticactions");
        Assert.IsTrue(baseUriFacets.Length == 1);
        var facets = handleFacets.Concat(hashtagFacets).Concat(uriFacets).Concat(baseUriFacets).ToArray();
        Assert.IsTrue(facets.Length == 11);
    }
}