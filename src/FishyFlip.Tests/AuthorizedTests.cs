// <copyright file="AuthorizedTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using Microsoft.Extensions.Logging.Debug;

namespace FishyFlip.Tests;

public class AuthorizedTests
{
    private ATProtocol proto;

    public AuthorizedTests()
    {
        string handle = Environment.GetEnvironmentVariable("BLUESKY_TEST_HANDLE") ?? throw new ArgumentNullException();
        string password = Environment.GetEnvironmentVariable("BLUESKY_TEST_PASSWORD") ?? throw new ArgumentNullException();
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri("https://drasticactions.ninja"))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        this.proto = atProtocolBuilder.Build();
        this.proto.Server.CreateSessionAsync(handle, password).Wait();
    }

    [Fact]
    public async Task GetPopularFeedGeneratorsAsync()
    {
        var result = await this.proto.Unspecced.GetPopularFeedGeneratorsAsync();
        result.Switch(
            success =>
            {
                Assert.True(success!.Feeds.Count() > 0);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetFeedAsyncTest()
    {
        var atUri = ATUri.Create("at://did:plc:hqmafuxb77d6cepxvqwlcekl/app.bsky.feed.generator/sandsky");
        var result = await this.proto.Feed.GetFeedAsync(atUri);
        result.Switch(
            success =>
            {
                Assert.True(success!.Feed.Count() > 0);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetFeedGeneratorAsyncTest()
    {
        var atUri = ATUri.Create("at://did:plc:hqmafuxb77d6cepxvqwlcekl/app.bsky.feed.generator/sandsky");
        var result = await this.proto.Feed.GetFeedGeneratorAsync(atUri);
        result.Switch(
            success =>
            {
                Assert.Equal(atUri.ToString(), success!.View.Uri.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetProfileAsyncTest()
    {
        var test1did = ATDid.Create("did:plc:ix37rgpewy5wtl5qzhunsldu");
        var result = await this.proto.Actor.GetProfileAsync(ATHandle.Create("test3.drasticactions.ninja"));
        result.Switch(
            success =>
            {
                Assert.Equal(test1did!.ToString(), success!.Did.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetProfilesAsyncWithHandlesTest()
    {
        var test1did = ATDid.Create("did:plc:ix37rgpewy5wtl5qzhunsldu");
        var test2did = ATDid.Create("did:plc:ljmpd62v6nzcm4gff4ovbbdt");
        var result = await this.proto.Actor.GetProfilesAsync(new[] { ATHandle.Create("test3.drasticactions.ninja"), ATHandle.Create("l-tan.bsky-v3.dolciss.net") });
        result.Switch(
            success =>
            {
                Assert.Equal(test1did!.ToString(), success!.Profiles[0]!.Did.ToString());
                Assert.Equal(test2did!.ToString(), success!.Profiles[1]!.Did.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetProfilesAsyncWithDidTest()
    {
        var test1did = ATDid.Create("did:plc:ix37rgpewy5wtl5qzhunsldu");
        var test2did = ATDid.Create("did:plc:adrmwce3psv74fm7p4tzf64k");
        var result = await this.proto.Actor.GetProfilesAsync(new[] { test1did, test2did });
        result.Switch(
            success =>
            {
                Assert.Equal(test1did!.ToString(), success!.Profiles[0]!.Did.ToString());
                Assert.Equal(test2did!.ToString(), success!.Profiles[1]!.Did.ToString());
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetPostsAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7ydijed6k2j");
        var postUri2 = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7yddkqyzk2j");
        var postCid = Cid.Decode("bafyreifnqptq4svrpo6xohngz6v7cskvecf5tokcrwwkjm2urtzg5aptpe");
        var postCid2 = Cid.Decode("bafyreig4ncon6rhnbr5gglmvuduegnk3wnvcxcepk4g3tlohhcpuzayb5m");
        var postThreadResult = await this.proto.Feed.GetPostsAsync(new[] { postUri, postUri2 });
        postThreadResult.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Posts[0].Cid);
                Assert.Equal(postCid2, success!.Posts[1].Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetPostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7ydijed6k2j");
        var postCid = Cid.Decode("bafyreifnqptq4svrpo6xohngz6v7cskvecf5tokcrwwkjm2urtzg5aptpe");
        var postThreadResult = await this.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
                       {
                           Assert.Equal(postCid, success!.Thread.Post.Cid);
                       },
            failed =>
                                  {
                                      Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
                                  });
    }

    [Fact]
    public async Task GetQuotePostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7ydijed6k2j");
        var postCid = Cid.Decode("bafyreifnqptq4svrpo6xohngz6v7cskvecf5tokcrwwkjm2urtzg5aptpe");
        var postThreadResult = await this.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Thread.Post.Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetExternalPostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7ydijed6k2j");
        var postCid = Cid.Decode("bafyreifnqptq4svrpo6xohngz6v7cskvecf5tokcrwwkjm2urtzg5aptpe");
        var postThreadResult = await this.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Thread.Post.Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetImagesPostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7yddkqyzk2j");
        var postCid = Cid.Decode("bafyreig4ncon6rhnbr5gglmvuduegnk3wnvcxcepk4g3tlohhcpuzayb5m");
        var postThreadResult = await this.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Thread.Post.Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetRecordWithMediaPostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7ydijed6k2j");
        var postCid = Cid.Decode("bafyreifnqptq4svrpo6xohngz6v7cskvecf5tokcrwwkjm2urtzg5aptpe");
        var postThreadResult = await this.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Thread.Post.Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetRepliesPostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:ix37rgpewy5wtl5qzhunsldu/app.bsky.feed.post/3k7ydijed6k2j");
        var postCid = Cid.Decode("bafyreifnqptq4svrpo6xohngz6v7cskvecf5tokcrwwkjm2urtzg5aptpe");
        var postThreadResult = await this.proto.Feed.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Thread.Post.Cid);
                Assert.True(success.Thread.Replies!.Count() > 0);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task CreatePostAsyncTest()
    {
        var test = await this.proto.Repo.CreatePostAsync("CreatePostAsyncTest", null, null, new[] { "en" });
        test.Switch(
            success =>
                       {
                           Assert.True(success!.Cid is not null);
                       },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task CreatePostWithImageAsyncTest()
    {
        var byteArray = Convert.FromBase64String(Samples.Base64Image);
        using var streamContent = new StreamContent(new MemoryStream(byteArray), byteArray.Length);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        var upload = await this.proto.Repo.UploadBlobAsync(streamContent);

        await upload.SwitchAsync(
            async success =>
            {
                Assert.True(success!.Blob is not null);
                var imagesEmbed = new ImagesEmbed(success.Blob.ToImage(), "CreatePostWithImageAsyncTest");
                var test = await this.proto.Repo.CreatePostAsync("CreatePostAsyncTest", null, imagesEmbed, new[] { "en" });
                test.Switch(
                        success2 =>
                        {
                            Assert.True(success2!.Cid is not null);
                            Assert.True(success2!.Uri is not null);
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

    [Fact]
    public async Task CreatePostWithFacetAsyncTest()
    {
        var prompt = "Link Text: ";
        var linkText = "CreatePostWithFacetAsyncTest";
        prompt = $"{prompt} {linkText}";
        int promptStart = prompt.IndexOf(linkText, StringComparison.InvariantCulture);
        int promptEnd = promptStart + Encoding.Default.GetBytes(linkText).Length;
        var facet = Facet.CreateFacetLink(promptStart, promptEnd, "https://drasticactions.ninja");
        var test = await this.proto.Repo.CreatePostAsync(prompt, new[] { facet }, null, new[] { "en" });
        test.Switch(
            success =>
                       {
                           Assert.True(success!.Cid is not null);
                       },
            failed =>
                                  {
                                      Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
                                  });
    }

    [Fact]
    public async Task CreatePostWithTagAsyncTest()
    {
        var prompt = "Hashtag Text: ";
        var linkText = "FishyFlipTest";
        prompt = $"{prompt} {linkText}";
        int promptStart = prompt.IndexOf(linkText, StringComparison.InvariantCulture);
        int promptEnd = promptStart + Encoding.Default.GetBytes(linkText).Length;
        var facet = Facet.CreateFacetHashtag(promptStart, promptEnd, "FishyFlipTest");
        var test = (await this.proto.Repo.CreatePostAsync(prompt, new[] { facet }, null, new[] { "en" })).HandleResult();
        Assert.True(test!.Cid is not null);
    }

    [Fact]
    public async Task CreateAndRemoveListTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var createList = (await this.proto.Repo.CreateCurateListAsync(randomName, "Test List", DateTime.UtcNow)).HandleResult();
        Assert.True(createList!.Cid is not null);
        Assert.True(createList!.Uri is not null);
        var repo = this.proto.SessionManager!.Session!.Did;
        var removeList = (await this.proto.Repo.DeleteListAsync(repo, createList.Uri.Rkey)).HandleResult();
        Assert.True(removeList is not null);
    }

    [Fact]
    public async Task CreateAndRemovePostTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var create = (await this.proto.Repo.CreatePostAsync(randomName)).HandleResult();
        Assert.True(create!.Cid is not null);
        Assert.True(create!.Uri is not null);
        var repo = this.proto.SessionManager!.Session!.Did;
        var remove = (await this.proto.Repo.DeletePostAsync(repo, create.Uri.Rkey)).HandleResult();
        Assert.True(remove is not null);
    }

    [Fact]
    public async Task CreateAndRemoveRepostTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var create = (await this.proto.Repo.CreatePostAsync(randomName)).HandleResult();
        Assert.True(create!.Cid is not null);
        Assert.True(create!.Uri is not null);

        var repost = (await this.proto.Repo.CreateRepostAsync(create.Cid, create.Uri)).HandleResult();
        Assert.True(repost!.Cid is not null);
        Assert.True(repost!.Uri is not null);

        var repo = this.proto.SessionManager!.Session!.Did;
        var removeRepost = (await this.proto.Repo.DeleteRepostAsync(repo, repost.Uri.Rkey)).HandleResult();
        Assert.True(removeRepost is not null);

        var remove = (await this.proto.Repo.DeletePostAsync(repo, create.Uri.Rkey)).HandleResult();
        Assert.True(remove is not null);
    }

    [Fact]
    public async Task CreateAndRemoveLikeTest()
    {
        var randomName = Guid.NewGuid().ToString();
        var create = (await this.proto.Repo.CreatePostAsync(randomName)).HandleResult();
        Assert.True(create!.Cid is not null);
        Assert.True(create!.Uri is not null);

        var like = (await this.proto.Repo.CreateLikeAsync(create.Cid, create.Uri)).HandleResult();
        Assert.True(like!.Cid is not null);
        Assert.True(like!.Uri is not null);

        var repo = this.proto.SessionManager!.Session!.Did;
        var removeLike = (await this.proto.Repo.DeleteLikeAsync(repo, like.Uri.Rkey)).HandleResult();
        Assert.True(removeLike is not null);

        var remove = (await this.proto.Repo.DeletePostAsync(repo, create.Uri.Rkey)).HandleResult();
        Assert.True(remove is not null);
    }

    [Fact]
    public async Task CreateAndRemoveFollowTest()
    {
        var follow1 = ATDid.Create("did:plc:up76ybimufzledmmhbv25wse");
        var follow = (await this.proto.Repo.CreateFollowAsync(follow1)).HandleResult();
        Assert.True(follow!.Cid is not null);
        Assert.True(follow!.Uri is not null);

        var repo = this.proto.SessionManager!.Session!.Did;
        var remove = (await this.proto.Repo.DeleteFollowAsync(repo, follow.Uri.Rkey)).HandleResult();
        Assert.True(remove is not null);
    }

    [Fact]
    public async Task CreateAndRemoveBlockTest()
    {
        var follow2 = ATDid.Create("did:plc:5x7vqoe5l3qbh3koqizdipst");
        var follow = (await this.proto.Repo.CreateBlockAsync(follow2)).HandleResult();
        Assert.True(follow!.Cid is not null);
        Assert.True(follow!.Uri is not null);

        var repo = this.proto.SessionManager!.Session!.Did;
        var remove = (await this.proto.Repo.DeleteBlockAsync(repo, follow.Uri.Rkey)).HandleResult();
        Assert.True(remove is not null);
    }

    [Fact]
    public async Task DescribeRepoTest()
    {
        var repo = this.proto.SessionManager!.Session!.Did;
        var describe = (await this.proto.Repo.DescribeRepoAsync(repo)).HandleResult();
        Assert.True(describe is not null);
        Assert.True(describe.HandleIsCorrect);
        Assert.True(describe.Did is not null);
        Assert.True(describe.Did!.ToString() == repo.ToString());
    }
}
