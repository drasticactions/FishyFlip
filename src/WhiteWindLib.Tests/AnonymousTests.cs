// <copyright file="AnonymousTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhiteWindLib;

namespace WhiteWindLib.Tests;

[TestClass]
public class AnonymousTests
{
    static ATProtocol proto;
    static WhiteWindBlog blog;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri("https://bsky.social"))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
        blog = new WhiteWindBlog(proto);
    }

    [TestMethod]
    public async Task GetAuthorPostsTest()
    {
        var did = ATDid.Create("did:plc:fzkpgpjj7nki7r5rhtmgzrez");
        var (result, error) = await blog.GetAuthorPostsAsync(did!);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
        Assert.IsTrue(result!.Records.Length > 0);
    }

    [TestMethod]
    public async Task GetAuthorPostTest()
    {
        var postUri = ATUri.Create("at://did:plc:fzkpgpjj7nki7r5rhtmgzrez/com.whtwnd.blog.entry/3kutsnrkgvk2o");
        var (result, error) = await blog.GetEntryAsync(postUri.Did!, postUri.Rkey);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
    }
}