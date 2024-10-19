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

    public AuthorizedTests()
    {
    }

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        string handle = (string?)context.Properties["BLUESKY_TEST_HANDLE"] ?? throw new ArgumentNullException();
        string password = (string?)context.Properties["BLUESKY_TEST_PASSWORD"] ?? throw new ArgumentNullException();
        string instance = "https://bsky.social";
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AuthorizedTests.proto = atProtocolBuilder.Build();
        AuthorizedTests.proto.AuthenticateWithPasswordAsync(handle, password).Wait();
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
    [DataRow("at://did:plc:z72i7hdynmk6r22z27h6tvur/app.bsky.feed.generator/whats-hot")]
    public async Task GetFeedAsyncTest(string feedGenerator)
    {
        var atUri = ATUri.Create(feedGenerator);
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
    [DataRow("at://did:plc:z72i7hdynmk6r22z27h6tvur/app.bsky.feed.generator/whats-hot")]
    public async Task GetFeedGeneratorAsyncTest(string feedGenerator)
    {
        var atUri = ATUri.Create(feedGenerator);
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
    [DataRow("drasticactions.xn--q9jyb4c")]
    public async Task GetProfileAsyncTest(string handle1)
    {
        var result = await AuthorizedTests.proto.Actor.GetProfileAsync(ATHandle.Create(handle1) ?? throw new ArgumentNullException(nameof(handle1)));
        result.Switch(
            success =>
            {
                Assert.AreEqual(success!.Handle, handle1);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    [DataRow("drasticactions.xn--q9jyb4c", "peepthisbot.bsky.social")]
    public async Task GetProfilesAsyncWithHandlesTest(string handle1, string handle2)
    {
        var result = await AuthorizedTests.proto.Actor.GetProfilesAsync(new[] { ATHandle.Create(handle1), ATHandle.Create(handle2) });
        result.Switch(
            success =>
            {
                Assert.AreEqual(handle1, success!.Profiles[0]!.Handle.ToString());
                Assert.AreEqual(handle2, success!.Profiles[1]!.Handle.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    [DataRow("did:plc:nrfz3bngz57p7g7yg6pbkyqr", "did:plc:okblbaji7rz243bluudjlgxt")]
    public async Task GetProfilesAsyncWithDidTest(string did1, string did2)
    {
        var test1did = ATDid.Create(did1);
        var test2did = ATDid.Create(did2);
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
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxmjdxlpl2r", "at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3knxdo7r2cj2m")]
    public async Task GetPostsAsyncTest(string quotePost, string quotePost2)
    {
        var postUri = ATUri.Create(quotePost);
        var postUri2 = ATUri.Create(quotePost2);
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
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l5bialwzz52f")]
    public async Task GetPostThreadAsyncTest(string postThread)
    {
        var postUri = ATUri.Create(postThread);
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
    [DataRow("", "")]
    public async Task GetQuotePostThreadAsyncTest(string quotePost, string quotePost2)
    {
        var postUri = ATUri.Create(quotePost);
        var postThreadResult = await AuthorizedTests.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.AreEqual(postUri.ToString(), success!.Thread.Post!.Uri.ToString());
                Assert.AreEqual(quotePost2, ((RecordViewEmbed)success!.Thread!.Post!.Embed!)!.Post.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    [DataRow("")]
    public async Task GetExternalPostThreadAsyncTest(string externalPost)
    {
        var postUri = ATUri.Create(externalPost);
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
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46tcntvgy2a")]
    public async Task GetImagesPostThreadAsyncTest(string imagesPost)
    {
        var postUri = ATUri.Create(imagesPost);
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
    [DataRow("at://did:plc:okblbaji7rz243bluudjlgxt/app.bsky.feed.post/3l46xtosyvf2y")]
    public async Task GetRecordWithMediaPostThreadAsyncTest(string mediaPost)
    {
        var postUri = ATUri.Create(mediaPost);
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
    [DataRow("")]
    public async Task GetRepliesPostThreadAsyncTest(string postThread)
    {
        var postUri = ATUri.Create(postThread);
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
    [DataRow("did:plc:nrfz3bngz57p7g7yg6pbkyqr")]
    public async Task CreateAndRemoveListTest(string followDid)
    {
        var randomName = Guid.NewGuid().ToString();
        var createList = (await AuthorizedTests.proto.Repo.CreateCurateListAsync(randomName, "Test List", DateTime.UtcNow)).HandleResult();
        Assert.IsTrue(createList!.Cid is not null);
        Assert.IsTrue(createList!.Uri is not null);

        var repo = AuthorizedTests.proto.Session!.Did;
        var lists = (await AuthorizedTests.proto.Graph.GetListsAsync(repo)).HandleResult();
        Assert.IsTrue(lists is not null);
        Assert.IsTrue(lists!.Lists.Count() > 0);

        var follow1 = ATDid.Create(followDid);
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
    [DataRow("did:plc:nrfz3bngz57p7g7yg6pbkyqr")]
    public async Task CreateAndRemoveFollowTest(string followDid)
    {
        var follow1 = ATDid.Create(followDid);
        var follow = (await AuthorizedTests.proto.Repo.CreateFollowAsync(follow1)).HandleResult();
        Assert.IsTrue(follow!.Cid is not null);
        Assert.IsTrue(follow!.Uri is not null);

        var remove = (await AuthorizedTests.proto.Repo.DeleteFollowAsync(follow.Uri.Rkey)).HandleResult();
        Assert.IsTrue(remove is not null);
    }

    [TestMethod]
    [DataRow("did:plc:nrfz3bngz57p7g7yg6pbkyqr")]
    public async Task CreateAndRemoveBlockTest(string blockDid)
    {
        var follow2 = ATDid.Create(blockDid);
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