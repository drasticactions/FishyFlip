// <copyright file="AnonymousTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging.Debug;

namespace FishyFlip.Tests;

public class AnonymousTests
{
    private ATProtocol proto;

    public AnonymousTests()
    {
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri("https://drasticactions.ninja"))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        this.proto = atProtocolBuilder.Build();
    }

    [Fact]
    public async Task GetPostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k237aznn4k22");
        var postCid = Cid.Decode("bafyreih4jqh2l5xnp5q6xqfxyqx73weiauuj2s2baoym4a6b3huxt4ynza");
        var post = await this.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetQuotePostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcdnc55k22");
        var postCid = Cid.Decode("bafyreiha4uutsovlvm3xisgzaendfyfhcr7p2xpuzc7an7xu5jeyrrifji");
        var post = await this.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Cid);
                Assert.True(success.Value?.Embed is not null);
                Assert.True(success.Value?.Embed.Type == Constants.EmbedTypes.Record);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetExternalPostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcwgl62222");
        var postCid = Cid.Decode("bafyreih26rnjlty7gxvdmq3m5wyb4im72irtdsfsiktdy3duvbo4hpo4l4");
        var post = await this.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Cid);
                Assert.True(success.Value?.Embed is not null);
                Assert.True(success.Value?.Embed.Type == Constants.EmbedTypes.External);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetImagesPostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcf5psyk22");
        var postCid = Cid.Decode("bafyreibbc52hzs47woy7zjy76rjmzd6w743dajlwxkuihawatnoev7jzei");
        var post = await this.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Cid);
                Assert.True(success.Value?.Embed is not null);
                Assert.True(success.Value?.Embed.Type == Constants.EmbedTypes.Images);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [Fact]
    public async Task GetRecordWithMediaPostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcee6joc22");
        var postCid = Cid.Decode("bafyreiezjt5bqt2xpcdfvisud7jrd4zuxygz4ssnuge3ddjcoptanvcnsa");
        var post = await this.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.Equal(postCid, success!.Cid);
                Assert.True(success.Value?.Embed is not null);
                Assert.True(success.Value?.Embed.Type == Constants.EmbedTypes.RecordWithMedia);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }
}