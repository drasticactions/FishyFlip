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
        string instance = "https://bsky.social";
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("WhiteWindLibTests"));
        AnonymousTests.proto = atProtocolBuilder.Build();
        blog = new WhiteWindBlog(proto);
    }

    [TestMethod]
    [DataRow("did:plc:fzkpgpjj7nki7r5rhtmgzrez")]
    public async Task GetAuthorPostsTest(string didString)
    {
        var did = ATDid.Create(didString);
        var (result, error) = await blog.GetAuthorEntriesAsync(did!);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
        Assert.IsTrue(result!.Records.Length > 0);
    }

    [TestMethod]
    [DataRow("at://did:plc:fzkpgpjj7nki7r5rhtmgzrez/com.whtwnd.blog.entry/3kudrxp52ps2a")]
    public async Task GetAuthorPostTest(string atDid)
    {
        var postUri = ATUri.Create(atDid);
        var (result, error) = await blog.GetEntryAsync(postUri.Did!, postUri.Rkey);
        Assert.IsNull(error);
        Assert.IsNotNull(result);
    }
}