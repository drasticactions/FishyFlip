// <copyright file="AuthorizedTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using Microsoft.Extensions.Logging.Debug;
using System.Net.Http;

namespace FishyFlip.Tests;

[TestClass]
public class AuthorizedTests
{
    static ATProtocol proto;
    static string handle;
    static string handle_2;
    static string did;
    static string did_2;
    static string post_thread;
    static string quote_post;
    static string quote_post_2;
    static string feed_generator;
    static string follow_did;
    static string block_did;
    static string media_post;
    static string images_post;
    static string external_post;

    public AuthorizedTests()
    {
    }

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        feed_generator = "at://did:plc:z72i7hdynmk6r22z27h6tvur/app.bsky.feed.generator/whats-hot";
        follow_did = "did:plc:nrfz3bngz57p7g7yg6pbkyqr";
        block_did = "did:plc:nrfz3bngz57p7g7yg6pbkyqr";
        media_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46xtosyvf2y";
        images_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46tcntvgy2a";
        external_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46sr63j7r2m";
        handle = (string?)context.Properties["BLUESKY_TEST_HANDLE"] ?? throw new ArgumentNullException();
        handle_2 = "peepthisbot.bsky.social";

        did = "did:plc:nrfz3bngz57p7g7yg6pbkyqr";
        did_2 = "did:plc:okblbaji7rz243bluudjlgxt";
        post_thread = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l5bialwzz52f";
        quote_post = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxmjdxlpl2r";
        quote_post_2 = "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxdo7r2cj2m";
        string password = (string?)context.Properties["BLUESKY_TEST_PASSWORD"] ?? throw new ArgumentNullException();
        string instance = "https://bsky.social";
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AuthorizedTests.proto = atProtocolBuilder.Build();
        AuthorizedTests.proto.AuthenticateWithPasswordAsync(AuthorizedTests.handle, password).Wait();
    }

    [TestMethod]
    public async Task GetPopularFeedGeneratorsAsync()
    {
        var result = await AuthorizedTests.proto.Unspecced.GetPopularFeedGeneratorsAsync();
        result.Switch(
            success =>
            {
                Assert.IsTrue(success!.Feeds.Count() > 0);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetFeedAsyncTest()
    {
        var atUri = ATUri.Create(AuthorizedTests.feed_generator);
        var result = await AuthorizedTests.proto.Feed.GetFeedAsync(atUri);
        result.Switch(
            success =>
            {
                Assert.IsTrue(success!.Feed.Count() > 0);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetFeedGeneratorAsyncTest()
    {
        var atUri = ATUri.Create(AuthorizedTests.feed_generator);
        var result = await AuthorizedTests.proto.Feed.GetFeedGeneratorAsync(atUri);
        result.Switch(
            success =>
            {
                Assert.AreEqual(atUri.ToString(), success!.View.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetProfileAsyncTest()
    {
        var result = await AuthorizedTests.proto.Actor.GetProfileAsync(ATHandle.Create(AuthorizedTests.handle) ?? throw new ArgumentNullException(nameof(AuthorizedTests.handle)));
        result.Switch(
            success =>
            {
                Assert.AreEqual(success!.Handle, AuthorizedTests.handle);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetProfilesAsyncWithHandlesTest()
    {
        var result = await AuthorizedTests.proto.Actor.GetProfilesAsync(new[] { ATHandle.Create(AuthorizedTests.handle), ATHandle.Create(AuthorizedTests.handle_2) });
        result.Switch(
            success =>
            {
                Assert.AreEqual(AuthorizedTests.handle, success!.Profiles[0]!.Handle.ToString());
                Assert.AreEqual(AuthorizedTests.handle_2.ToString(), success!.Profiles[1]!.Handle.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetProfilesAsyncWithDidTest()
    {
        var test1did = ATDid.Create(AuthorizedTests.did);
        var test2did = ATDid.Create(AuthorizedTests.did_2);
        var result = await AuthorizedTests.proto.Actor.GetProfilesAsync(new[] { test1did, test2did });
        result.Switch(
            success =>
            {
                Assert.AreEqual(test1did!.ToString(), success!.Profiles[0]!.Did.ToString());
                Assert.AreEqual(test2did!.ToString(), success!.Profiles[1]!.Did.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetPostsAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.quote_post);
        var postUri2 = ATUri.Create(AuthorizedTests.quote_post_2);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostsAsync(new[] { postUri, postUri2 });
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Posts[0].Uri.ToString());
                Assert.AreEqual(postUri2.ToString(), success!.Posts[1].Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetPostThreadAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.post_thread);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
                       {
                           Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
                       },
            failed =>
                                  {
                                      Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
                                  });
    }

    [TestMethod]
    public async Task GetQuotePostThreadAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.quote_post);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
                Assert.AreEqual(AuthorizedTests.quote_post_2, ((RecordViewEmbed)success!.Thread!.Post!.Embed!)!.Post.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetExternalPostThreadAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.external_post);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetImagesPostThreadAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.images_post);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetRecordWithMediaPostThreadAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.media_post);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetRepliesPostThreadAsyncTest()
    {
        var postUri = ATUri.Create(AuthorizedTests.post_thread);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
                Assert.IsTrue(success.Thread.Replies!.Count() > 0);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task CreatePostAsyncTest()
    {
        var test = await AuthorizedTests.proto.Repo.CreatePostAsync("CreatePostAsyncTest", null, null, new[] { "en" });
        test.Switch(
            success =>
                       {
                           Assert.IsTrue(success!.Cid is not null);
                       },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task CreatePostWithReplyAsyncTest()
    {
        (var test, var error) = await AuthorizedTests.proto.Repo.CreatePostAsync("CreatePostAsyncTest", null, null, null, new[] { "en" });
        Assert.IsTrue(test!.Cid is not null);
        (var reply, error) = await AuthorizedTests.proto.Repo.CreatePostAsync("CreatePostAsyncTestReply", new Reply(new ReplyRef(test!.Cid, test.Uri!), new ReplyRef(test!.Cid, test.Uri!)), null, null, new[] { "en" });
        Assert.IsTrue(reply!.Cid is not null);
    }

    [TestMethod]
    public async Task CreatePostWithImageAsyncTest()
    {
        var byteArray = Convert.FromBase64String(Samples.Base64Image);
        using var streamContent = new StreamContent(new MemoryStream(byteArray), byteArray.Length);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        var upload = await AuthorizedTests.proto.Repo.UploadBlobAsync(streamContent);

        await upload.SwitchAsync(
            async success =>
            {
                Assert.IsTrue(success!.Blob is not null);
                var imagesEmbed = new ImagesEmbed(success.Blob.ToImage(), "CreatePostWithImageAsyncTest");
                var test = await AuthorizedTests.proto.Repo.CreatePostAsync("CreatePostAsyncTest", null, imagesEmbed, new[] { "en" });
                test.Switch(
                        success2 =>
                        {
                            Assert.IsTrue(success2!.Cid is not null);
                            Assert.IsTrue(success2!.Uri is not null);
                        },
                        failed2 =>
                        {
                            Assert.Fail($"{failed2.StatusCode}: {failed2.Detail}");
                        });
            },
            async failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task CreatePostWithFacetAsyncTest()
    {
        var prompt = "Link Text: ";
        var linkText = "CreatePostWithFacetAsyncTest";
        prompt = $"{prompt} {linkText}";
        int promptStart = prompt.IndexOf(linkText, StringComparison.InvariantCulture);
        int promptEnd = promptStart + Encoding.Default.GetBytes(linkText).Length;
        var facet = Facet.CreateFacetLink(promptStart, promptEnd, "https://drasticactions.ninja");
        var test = await AuthorizedTests.proto.Repo.CreatePostAsync(prompt, new[] { facet }, null, new[] { "en" });
        test.Switch(
            success =>
                       {
                           Assert.IsTrue(success!.Cid is not null);
                       },
            failed =>
                                  {
                                      Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
                                  });
    }

    [TestMethod]
    public async Task CreatePostWithTagAsyncTest()
    {
        var prompt = "Hashtag Text: ";
        var linkText = "FishyFlipTest";
        prompt = $"{prompt} {linkText}";
        int promptStart = prompt.IndexOf(linkText, StringComparison.InvariantCulture);
        int promptEnd = promptStart + Encoding.Default.GetBytes(linkText).Length;
        var facet = Facet.CreateFacetHashtag(promptStart, promptEnd, "FishyFlipTest");
        var test = (await AuthorizedTests.proto.Repo.CreatePostAsync(prompt, new[] { facet }, null, new[] { "en" })).HandleResult();
        Assert.IsTrue(test!.Cid is not null);
    }

    [TestMethod]
    public async Task CreateAndRemoveListTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var createList = (await AuthorizedTests.proto.Repo.CreateCurateListAsync(randomName, "Test List", DateTime.UtcNow)).HandleResult();
        Assert.IsTrue(createList!.Cid is not null);
        Assert.IsTrue(createList!.Uri is not null);

        var repo = AuthorizedTests.proto.Session!.Did;
        var lists = (await AuthorizedTests.proto.Graph.GetListsAsync(repo)).HandleResult();
        Assert.IsTrue(lists is not null);
        Assert.IsTrue(lists!.Lists.Count() > 0);

        var follow1 = ATDid.Create(AuthorizedTests.follow_did);
        var follow = (await AuthorizedTests.proto.Repo.CreateListItemAsync(follow1, createList.Uri)).HandleResult();
        Assert.IsTrue(follow!.Cid is not null);
        Assert.IsTrue(follow!.Uri is not null);

        await Task.Delay(2500);
        var list = (await AuthorizedTests.proto.Graph.GetListAsync(createList.Uri)).HandleResult();
        Assert.IsTrue(list is not null);
        Assert.IsTrue(list!.Items.Count() > 0);

        var removeListItem = (await AuthorizedTests.proto.Repo.DeleteListItemAsync(follow.Uri.Rkey)).HandleResult();
        Assert.IsTrue(removeListItem is not null);

        var removeList = (await AuthorizedTests.proto.Repo.DeleteListAsync(createList.Uri.Rkey)).HandleResult();
        Assert.IsTrue(removeList is not null);
    }

    [TestMethod]
    public async Task CreateAndRemovePostTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var create = (await AuthorizedTests.proto.Repo.CreatePostAsync(randomName)).HandleResult();
        Assert.IsTrue(create!.Cid is not null);
        Assert.IsTrue(create!.Uri is not null);
        var repo = AuthorizedTests.proto!.Session!.Did;
        var remove = (await AuthorizedTests.proto.Repo.DeletePostAsync(create.Uri.Rkey)).HandleResult();
        Assert.IsTrue(remove is not null);
    }

    [TestMethod]
    public async Task CreateAndRemoveRepostTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var create = (await AuthorizedTests.proto.Repo.CreatePostAsync(randomName)).HandleResult();
        Assert.IsTrue(create!.Cid is not null);
        Assert.IsTrue(create!.Uri is not null);

        var repost = (await AuthorizedTests.proto.Repo.CreateRepostAsync(create.Cid, create.Uri)).HandleResult();
        Assert.IsTrue(repost!.Cid is not null);
        Assert.IsTrue(repost!.Uri is not null);

        var repo = AuthorizedTests.proto.Session!.Did;
        var removeRepost = (await AuthorizedTests.proto.Repo.DeleteRepostAsync(repost.Uri.Rkey)).HandleResult();
        Assert.IsTrue(removeRepost is not null);

        var remove = (await AuthorizedTests.proto.Repo.DeletePostAsync(create.Uri.Rkey)).HandleResult();
        Assert.IsTrue(remove is not null);
    }

    [TestMethod]
    public async Task CreateAndRemoveLikeTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var create = (await AuthorizedTests.proto.Repo.CreatePostAsync(randomName)).HandleResult();
        Assert.IsTrue(create!.Cid is not null);
        Assert.IsTrue(create!.Uri is not null);

        var like = (await AuthorizedTests.proto.Repo.CreateLikeAsync(create.Cid, create.Uri)).HandleResult();
        Assert.IsTrue(like!.Cid is not null);
        Assert.IsTrue(like!.Uri is not null);

        var removeLike = (await AuthorizedTests.proto.Repo.DeleteLikeAsync(like.Uri.Rkey)).HandleResult();
        Assert.IsTrue(removeLike is not null);

        var remove = (await AuthorizedTests.proto.Repo.DeletePostAsync(create.Uri.Rkey)).HandleResult();
        Assert.IsTrue(remove is not null);
    }

    [TestMethod]
    public async Task CreateAndRemoveFollowTest()
    {
        var follow1 = ATDid.Create(AuthorizedTests.follow_did);
        var follow = (await AuthorizedTests.proto.Repo.CreateFollowAsync(follow1)).HandleResult();
        Assert.IsTrue(follow!.Cid is not null);
        Assert.IsTrue(follow!.Uri is not null);

        var remove = (await AuthorizedTests.proto.Repo.DeleteFollowAsync(follow.Uri.Rkey)).HandleResult();
        Assert.IsTrue(remove is not null);
    }

    [TestMethod]
    public async Task CreateAndRemoveBlockTest()
    {
        var follow2 = ATDid.Create(AuthorizedTests.block_did);
        var follow = (await AuthorizedTests.proto.Repo.CreateBlockAsync(follow2)).HandleResult();
        Assert.IsTrue(follow!.Cid is not null);
        Assert.IsTrue(follow!.Uri is not null);

        var remove = (await AuthorizedTests.proto.Repo.DeleteBlockAsync(follow.Uri.Rkey)).HandleResult();
        Assert.IsTrue(remove is not null);
    }

    [TestMethod]
    public async Task DescribeRepoTest()
    {
        var repo = AuthorizedTests.proto!.Session!.Did;
        var describe = (await AuthorizedTests.proto.Repo.DescribeRepoAsync(repo)).HandleResult();
        Assert.IsTrue(describe is not null);
        Assert.IsTrue(describe.HandleIsCorrect);
        Assert.IsTrue(describe.Did is not null);
        Assert.IsTrue(describe.Did!.ToString() == repo.ToString());
    }

    [TestMethod]
    public async Task CreatePostWithThreadGate()
    {
        var (post, error) = await AuthorizedTests.proto.Repo.CreatePostAsync("prompt", null, null, new[] { "en" });
        Assert.IsNull(error);
        Assert.IsNotNull(post);
        Assert.IsNotNull(post!.Uri);
        var threadPostReasons = new[] { ThreadGateReason.CreateFollowingRule(), ThreadGateReason.CreateMentionRule() };
        var (threadGate, _) = await AuthorizedTests.proto.Repo.CreateThreadGateAsync(post!.Uri!, threadPostReasons);
        Assert.IsNotNull(threadGate);
        Assert.IsNotNull(threadGate.Uri);
        Assert.IsNotNull(threadGate.Cid);
    }
}