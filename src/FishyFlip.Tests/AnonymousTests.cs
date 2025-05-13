// <copyright file="AnonymousTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;
using FishyFlip.Lexicon.App.Bsky.Embed;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.App.Bsky.Labeler;
using FishyFlip.Lexicon.App.Bsky.Richtext;
using FishyFlip.Lexicon.Com.Atproto.Repo;
using Microsoft.Extensions.Logging.Debug;

namespace FishyFlip.Tests;

/// <summary>
/// Tests for anonymous access to the ATProtocol API.
/// </summary>
[TestClass]
public class AnonymousTests
{
    private static ATProtocol? proto;

    /// <summary>
    /// Initialize the test class.
    /// </summary>
    /// <param name="context"><see cref="TestContext"/>.</param>
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
    }

    /// <summary>
    /// Test the GetRecordAsync method. Gets the posts and verifies the data.
    /// </summary>
    /// <param name="atUri">The ATUri.</param>
    /// <param name="embedType">The type of data that should be embedded.</param>
    /// <returns>Task.</returns>
    [TestMethod]
    [DataRow("at://did:web:zio.sh/app.bsky.feed.post/3lnvkn5gdzc2n", "")]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q4gqbk2y", "")]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxcq7bwwo2j", EmbedRecord.RecordType)]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46sr63j7r2m", EmbedExternal.RecordType)]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q57gcs2k", EmbedImages.RecordType)]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46xtosyvf2y", EmbedVideo.RecordType)]
    public async Task TestPostAsync(string atUri, string embedType)
    {
        var postUri = ATUri.Create(atUri);
        Assert.IsNotNull(postUri);
        Assert.IsNotNull(postUri.Identifier);
        var post = await AnonymousTests.proto!.GetRecordAsync(postUri.Identifier, Post.RecordType, postUri.Rkey);
        post.Switch(
            success =>
            {
                var value = (Post)success!.Value!;
                Assert.AreEqual(postUri, success!.Uri!);
                Assert.IsNotNull(success.Value);
                Assert.AreEqual(success.Value.Type, Post.RecordType);

                if (!string.IsNullOrEmpty(embedType))
                {
                    Assert.IsNotNull(value.Embed);
                    Assert.AreEqual(value.Embed.Type, embedType);
                    switch (value.Embed.Type)
                    {
                        case EmbedRecord.RecordType:
                            var recordEmbed = (EmbedRecord)value.Embed;
                            Assert.IsNotNull(recordEmbed);
                            Assert.IsNotNull(recordEmbed.Record);
                            Assert.IsNotNull(recordEmbed.Record.Cid);
                            Assert.IsNotNull(recordEmbed.Record.Uri);
                            break;
                        case EmbedExternal.RecordType:
                            var externalEmbed = (EmbedExternal)value.Embed;
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
                        case EmbedImages.RecordType:
                            var imagesEmbed = (EmbedImages)value.Embed;
                            Assert.IsNotNull(imagesEmbed);
                            Assert.IsNotNull(imagesEmbed.Images);
                            foreach (var image in imagesEmbed.Images)
                            {
                                Assert.IsNotNull(image);
                                image.ThrowIfNull();
                                image?.ImageValue?.Ref.ThrowIfNull();
                                Assert.IsTrue(!string.IsNullOrEmpty(image?.ImageValue?.MimeType));
                            }

                            break;
                        case EmbedVideo.RecordType:
                            var videoEmbed = (EmbedVideo)value.Embed;
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
            failed => { Assert.Fail($"{failed.StatusCode}: {failed.Detail}"); });
    }

    /// <summary>
    /// Tests DescribeRepoAsync to verify the repo exists.
    /// </summary>
    /// <param name="did">The DID to test.</param>
    /// <returns>Task.</returns>
    [TestMethod]
    [DataRow("did:plc:okblbaji7rz243bluudjlgxt")]
    public async Task DescribeRepoTest(string did)
    {
        var repo = ATDid.Create(did);
        var describe = (await AnonymousTests.proto!.DescribeRepoAsync(repo!)).HandleResult();
        Assert.IsTrue(describe is not null);
        Assert.IsTrue(describe.HandleIsCorrect);
        Assert.IsTrue(describe.Did is not null);
        Assert.AreEqual(describe.Did!, repo!);
    }

    /// <summary>
    /// Verify Markdown post parsing.
    /// </summary>
    [TestMethod]
    public void MarkdownPostTest()
    {
        var markdownPost =
            @"Markdown Test: [FishyFlip](https://drasticactions.github.io/FishyFlip), #FishyFlip, [@drasticactions.dev](did:plc:yhgc5rlqhoezrx6fbawajxlh)";
        var post = MarkdownPost.Parse(markdownPost);
        Assert.IsTrue(post.OriginalMarkdown == markdownPost);
        Assert.IsTrue(post.Post == "Markdown Test: FishyFlip, #FishyFlip, @drasticactions.dev");
        Assert.IsTrue(post.Facets.Count == 3);
        Assert.IsTrue(post.Facets[0].Features![0]!.Type == FishyFlip.Lexicon.App.Bsky.Richtext.Link.RecordType);
        Assert.IsTrue(((Link)post.Facets[0].Features![0]!).Uri == "https://drasticactions.github.io/FishyFlip");
        Assert.IsTrue(post.Facets[0].Index!.ByteStart == 15);
        Assert.IsTrue(post.Facets[0].Index!.ByteEnd == 24);
        Assert.IsTrue(post.Facets[1].Features![0]!.Type == FishyFlip.Lexicon.App.Bsky.Richtext.Mention.RecordType);
        Assert.IsTrue(Equals(((Mention)post.Facets[1].Features![0]!).Did!, ATDid.Create("did:plc:yhgc5rlqhoezrx6fbawajxlh")!));
        Assert.IsTrue(post.Facets[1].Index!.ByteStart == 38);
        Assert.IsTrue(post.Facets[1].Index!.ByteEnd == 57);
        Assert.IsTrue(post.Facets[2].Features![0]!.Type == FishyFlip.Lexicon.App.Bsky.Richtext.Tag.RecordType);
        Assert.IsTrue(((Tag)post.Facets[2].Features![0]!).TagValue == "FishyFlip");
        Assert.IsTrue(post.Facets[2].Index!.ByteStart == 26);
        Assert.IsTrue(post.Facets[2].Index!.ByteEnd == 36);
    }

    /// <summary>
    /// Validate Facet Helpers.
    /// </summary>
    [TestMethod]
    public void ValidateFacetHelpers()
    {
        var daDev = new FacetActorIdentifier(ATHandle.Create("drasticactions.dev")!, ATDid.Create("did:plc:okblbaji7rz243bluudjlgxt")!);
        var daJp = new FacetActorIdentifier(ATHandle.Create("drasticactions.jp")!, ATDid.Create("did:plc:okblbaji7rz243bluudjl2bt")!);

        var postText =
            "@drasticactions.dev This is a #test #test of #testing the #FishyFlip #API. https://github.com/drasticactions DAHome. @drasticactions.jp https://github.com/drasticactions/FishyFlip @drasticactions.dev Weee!";
        var postHandles = ATHandle.FromPostText(postText);
        Assert.IsTrue(postHandles.Length == 2);
        Assert.IsTrue(postHandles[0].Handle == "drasticactions.dev");
        Assert.IsTrue(postHandles[1].Handle == "drasticactions.jp");

        var handleFacets = Facet.ForMentions(postText, new FacetActorIdentifier[] { daDev, daJp });

        Assert.IsTrue(handleFacets.Length == 3);
        Assert.IsTrue(handleFacets[0].Index!.ByteStart == 0);
        Assert.IsTrue(handleFacets[0].Index!.ByteEnd == 19);
        Assert.IsTrue(((Mention)handleFacets[0].Features![0]!).Did! == daDev.Did);

        Assert.IsTrue(handleFacets[1].Index!.ByteStart == 117);
        Assert.IsTrue(handleFacets[1].Index!.ByteEnd == 135);
        Assert.IsTrue(((Mention)handleFacets[1].Features![0]!).Did! == daJp.Did);

        Assert.IsTrue(((Mention)handleFacets[2].Features![0]!).Did! == daDev.Did);
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

    /// <summary>
    /// Tests the GetEntryAsync method and validates the post exists.
    /// </summary>
    /// <param name="atDid">The ATDid.</param>
    /// <returns>Task.</returns>
    [TestMethod]
    [DataRow("at://did:plc:fzkpgpjj7nki7r5rhtmgzrez/com.whtwnd.blog.entry/3kudrxp52ps2a")]
    public async Task GetAuthorPostTest(string atDid)
    {
        var postUri = ATUri.Create(atDid);
        var (result, error) = await AnonymousTests.proto!.ComWhtwndBlog.GetEntryAsync(postUri.Did!, postUri.Rkey);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
    }

    /// <summary>
    /// Tests the labeler.
    /// </summary>
    /// <param name="atDid">The ATDid.</param>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task LabelDetails()
    {
        var did1 = ATDid.Create("did:plc:hysbs7znfgxyb4tsvetzo4sk")!;
        var (result, error) = await AnonymousTests.proto!.Labeler.GetServicesAsync([did1]);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Views?.Count == 1);
        var labelerView = (LabelerView)result.Views![0];
        Assert.IsNull(labelerView.Viewer);

        (result, error) = await AnonymousTests.proto!.Labeler.GetServicesAsync([did1], true);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Views?.Count == 1);
        var labelerViewDetailed = (LabelerViewDetailed)result.Views![0];
        Assert.IsTrue(labelerViewDetailed.Policies?.LabelValues?.Any());
    }

    /// <summary>
    /// Validate ATUri.
    /// </summary>
    [TestMethod]
    public void ATUriValidation()
    {
        var atUri = ATUri.Create("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q4gqbk2y");
        var atUri2 = ATUri.Create("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxcq7bwwo2j");
        var test = atUri.Equals(atUri);
        Assert.AreEqual(atUri, atUri);
        Assert.AreNotEqual(atUri, atUri2);

        var atDid = ATDid.Create("did:plc:okblbaji7rz243bluudjlgxt");
        var atDid2 = ATDid.Create("did:web:drasticactions.dev");

        Assert.AreEqual(atDid, atDid);
        Assert.AreNotEqual(atDid, atDid2);

        var atHandle = ATHandle.Create("drasticactions.dev");
        var atHandle2 = ATHandle.Create("drasticactions.jp");

        Assert.AreEqual(atHandle, atHandle);
        Assert.AreNotEqual(atHandle, atHandle2);
    }

    /// <summary>
    /// Validate JSON.
    /// </summary>
    /// <returns>Task.</returns>
    [TestMethod]
    public async Task JsonValidation()
    {
        var postUri = ATUri.Create("at://gooffkings.bsky.social/app.bsky.feed.post/3lgcbxwyj6s2t")!;
        (var post, var error) = await AnonymousTests.proto!.Feed.GetPostThreadAsync(postUri);
        Assert.IsNull(error);
        Assert.IsNotNull(post);
        var testing1 = post.ToJson();
        var test2 = GetPostThreadOutput.FromJson(testing1);
        Assert.IsNotNull(test2);
        Assert.IsTrue(((ThreadViewPost)post.Thread).Replies!.Count == ((ThreadViewPost)test2!.Thread).Replies!.Count);
    }

    /// <summary>
    /// Validate the in-memory cache.
    /// </summary>
    /// <param name="atUri">ATUri.</param>
    /// <returns>Task.</returns>
    [TestMethod]
    [DataRow("at://zio.sh/app.bsky.feed.post/3lnvkn5gdzc2n")]
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3kv25q4gqbk2y")]
    public async Task ValidateInMemoryCache(string atUri)
    {
        var postUri = ATUri.Create(atUri);
        var post = await AnonymousTests.proto!.GetRecordAsync(postUri.Identifier!, Post.RecordType, postUri.Rkey);
        Assert.IsNotNull(post);
        Assert.IsTrue(post is not null);
        Assert.IsTrue(post!.Value is not null);
        var cacheDidDoc = AnonymousTests.proto!.Options.DidDocCache.GetAsync(postUri.Identifier!);
        Assert.IsNotNull(cacheDidDoc);
        var post2 = await AnonymousTests.proto!.GetRecordAsync(postUri.Identifier!, Post.RecordType, postUri.Rkey);
        Assert.IsNotNull(post2);
        var count = await AnonymousTests.proto!.Options.DidDocCache.CountAsync();
        Assert.IsTrue(count == 1);
    }
}