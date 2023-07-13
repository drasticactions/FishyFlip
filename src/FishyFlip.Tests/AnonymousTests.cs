// <copyright file="AnonymousTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Models;
using Microsoft.Extensions.Logging.Debug;
using static FishyFlip.Tools.CarDecoder;

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

    [Fact]
    public async Task GetJpegBlobTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var postCid = Cid.Decode("bafkreibcidyi7immx2komo7yccy2ecq3lpckq4mnypqp7euljd3tjomdwa");
        var blob = await this.proto.Sync.GetBlobAsync(postUri, postCid);
        blob.Switch(
           success =>
           {
               Assert.True(success!.Data!.Length > 0);
               Assert.True(success!.Data!.Length == 134538);
           },
           failed =>
           {
               Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
           });
    }

    [Fact]
    public async Task GetHeadTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var blob = await this.proto.Sync.GetHeadAsync(postUri);
        blob.Switch(
           success =>
           {
               Assert.True(success!.Root is not null);
           },
           failed =>
           {
               Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
           });
    }

    [Fact]
    public async Task GetRepoTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var blob = await this.proto.Sync.GetRepoAsync(postUri, HandleProgressStatus);
        blob.Switch(
           success =>
           {
               // Assert.True(success!.Any());
           },
           failed =>
           {
               Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
           });
    }

    [Fact]
    public async Task GetCheckoutTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var blob = await this.proto.Sync.GetCheckoutAsync(postUri, HandleProgressStatus);
        blob.Switch(
           success =>
           {
               // Assert.True(success!.Any());
           },
           failed =>
           {
               Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
           });
    }

    private static void HandleProgressStatus(CarProgressStatusEvent e)
    {
        var cid = e.Cid;
        var bytes = e.Bytes;
        var test = CBORObject.DecodeFromBytes(bytes);
    }
}