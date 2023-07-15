// <copyright file="AuthorizedTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    public async Task GetPostThreadAsyncTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k237aznn4k22");
        var postCid = Cid.Decode("bafyreih4jqh2l5xnp5q6xqfxyqx73weiauuj2s2baoym4a6b3huxt4ynza");
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcdnc55k22");
        var postCid = Cid.Decode("bafyreiha4uutsovlvm3xisgzaendfyfhcr7p2xpuzc7an7xu5jeyrrifji");
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcwgl62222");
        var postCid = Cid.Decode("bafyreih26rnjlty7gxvdmq3m5wyb4im72irtdsfsiktdy3duvbo4hpo4l4");
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcwgl62222");
        var postCid = Cid.Decode("bafyreih26rnjlty7gxvdmq3m5wyb4im72irtdsfsiktdy3duvbo4hpo4l4");
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcee6joc22");
        var postCid = Cid.Decode("bafyreiezjt5bqt2xpcdfvisud7jrd4zuxygz4ssnuge3ddjcoptanvcnsa");
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k233apjyxk2c");
        var postCid = Cid.Decode("bafyreidaauhr3wsdximedjc3agkwbwdu3bbupci7urrop6tz6xqputugt4");
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
}
