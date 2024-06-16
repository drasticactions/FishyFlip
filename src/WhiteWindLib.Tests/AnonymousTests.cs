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
    static string did;

    static string aturi;

    static ATProtocol proto;
    static WhiteWindBlog blog;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        did = (string?)context.Properties["BLUESKY_TEST_DID"] ?? throw new ArgumentNullException();
        aturi = (string?)context.Properties["BLUESKY_TEST_ATURI"] ?? throw new ArgumentNullException();
        string instance = (string?)context.Properties["BLUESKY_INSTANCE_URL"] ?? throw new ArgumentNullException();
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("WhiteWindLibTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
        blog = new WhiteWindBlog(proto);
    }

    [TestMethod]
    public async Task GetAuthorPostsTest()
    {
        var did = ATDid.Create(AnonymousTests.did);
        var (result, error) = await blog.GetAuthorEntriesAsync(did!);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
        Assert.IsTrue(result!.Records.Length > 0);
    }

    [TestMethod]
    public async Task GetAuthorPostTest()
    {
        var postUri = ATUri.Create(AnonymousTests.aturi);
        var (result, error) = await blog.GetEntryAsync(postUri.Did!, postUri.Rkey);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
    }
}