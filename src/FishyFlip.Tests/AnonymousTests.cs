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

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        string instance = (string?)context.Properties["BLUESKY_INSTANCE_URL"] ?? throw new ArgumentNullException();
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
    }

    [TestMethod]
    public async Task GetBlocksAsyncTest()
    {
        var atDid = ATDid.Create("did:plc:wrrbtigjwpykuwzqsypnpazr");
        var postCid1 = Cid.Decode("bafyreibby2anauk6ef2ntmeyebeb3yosncathvohhjrb7jmxfpyljyeq2e");
        var postCid2 = Cid.Decode("bafyreiausj2iabpfs2mbmp2qtaszd2jokmsogto7z6zrz3pkncx3emyx4m");

        var oncardecoded = new OnCarDecoded((e) => {
        });

        var result = await AnonymousTests.proto.Sync.GetBlocksAsync(atDid, new[] { postCid1.ToString(), postCid2.ToString() }, oncardecoded);

        result.Switch(
            success =>
            {
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task DownloadBlocksAsyncTest()
    {
        var atDid = ATDid.Create("did:plc:wrrbtigjwpykuwzqsypnpazr");
        var postCid1 = Cid.Decode("bafyreibby2anauk6ef2ntmeyebeb3yosncathvohhjrb7jmxfpyljyeq2e");
        var postCid2 = Cid.Decode("bafyreiausj2iabpfs2mbmp2qtaszd2jokmsogto7z6zrz3pkncx3emyx4m");

        var result = await AnonymousTests.proto.Sync.DownloadBlocksAsync(atDid, new[] { postCid1.ToString(), postCid2.ToString() });

        result.Switch(
            success =>
            {
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetPostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k237aznn4k22");
        var postCid = Cid.Decode("bafyreih4jqh2l5xnp5q6xqfxyqx73weiauuj2s2baoym4a6b3huxt4ynza");
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postCid.ToString(), success!.Cid);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetQuotePostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcdnc55k22");
        var postCid = Cid.Decode("bafyreiha4uutsovlvm3xisgzaendfyfhcr7p2xpuzc7an7xu5jeyrrifji");
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postCid.ToString(), success!.Cid);
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcwgl62222");
        var postCid = Cid.Decode("bafyreih26rnjlty7gxvdmq3m5wyb4im72irtdsfsiktdy3duvbo4hpo4l4");
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postCid.ToString(), success!.Cid);
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
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcf5psyk22");
        var postCid = Cid.Decode("bafyreibbc52hzs47woy7zjy76rjmzd6w743dajlwxkuihawatnoev7jzei");
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postCid.ToString(), success!.Cid);
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.Images, success.Value?.Embed.Type);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetRecordWithMediaPostRecordTest()
    {
        var postUri = ATUri.Create("at://did:plc:7i5tmb4yfkznrn7whz4dg4gz/app.bsky.feed.post/3k2dcee6joc22");
        var postCid = Cid.Decode("bafyreiezjt5bqt2xpcdfvisud7jrd4zuxygz4ssnuge3ddjcoptanvcnsa");
        var post = await AnonymousTests.proto.Repo.GetPostAsync(postUri.Did!, postUri.Rkey);
        post.Switch(
            success =>
            {
                Assert.AreEqual(postCid.ToString(), success!.Cid);
                Assert.IsTrue(success.Value?.Embed is not null);
                Assert.AreEqual(Constants.EmbedTypes.RecordWithMedia, success.Value?.Embed.Type);
            },
            failed =>
            {
                Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
            });
    }

    [TestMethod]
    public async Task GetJpegBlobTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var postCid = Cid.Decode("bafkreibcidyi7immx2komo7yccy2ecq3lpckq4mnypqp7euljd3tjomdwa");
        var blob = await AnonymousTests.proto.Sync.GetBlobAsync(postUri, postCid);
        blob.Switch(
           success =>
           {
               Assert.IsTrue(success!.Data!.Length > 0);
               Assert.AreEqual(134538, success!.Data!.Length);
           },
           failed =>
           {
               Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
           });
    }

    [TestMethod]
    public async Task GetHeadTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var blob = await AnonymousTests.proto.Sync.GetHeadAsync(postUri);
        blob.Switch(
           success =>
           {
               Assert.IsTrue(success!.Root is not null);
           },
           failed =>
           {
               Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
           });
    }

    [TestMethod]
    public async Task GetRepoTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var blob = await AnonymousTests.proto.Sync.GetRepoAsync(postUri, HandleProgressStatus);
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

    [TestMethod]
    public async Task GetCheckoutTest()
    {
        var postUri = ATDid.Create("did:plc:7i5tmb4yfkznrn7whz4dg4gz");
        var blob = await AnonymousTests.proto.Sync.GetCheckoutAsync(postUri, HandleProgressStatus);
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

    [TestMethod]
    public async Task DescribeRepoTest()
    {
        var repo = ATDid.Create("did:plc:up76ybimufzledmmhbv25wse");
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