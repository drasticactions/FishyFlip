// <copyright file="AuthorizedTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.App.Bsky.Actor;
using FishyFlip.Lexicon.App.Bsky.Embed;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.App.Bsky.Unspecced;
using FishyFlip.Lexicon.Com.Atproto.Repo;
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
        var result = await AuthorizedTests.proto.GetPopularFeedGeneratorsAsync();
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
        var result = await AuthorizedTests.proto.GetFeedAsync(atUri);
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
        var result = await AuthorizedTests.proto.GetFeedGeneratorAsync(atUri);
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
        var result = await AuthorizedTests.proto.GetProfileAsync(ATHandle.Create(handle1) ?? throw new ArgumentNullException(nameof(handle1)));
        result.Switch(
            success =>
            {
                Assert.AreEqual(success!.Handle!.ToString(), handle1);
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
        var result = await AuthorizedTests.proto.GetProfilesAsync(new List<ATIdentifier> { ATHandle.Create(handle1)!, ATHandle.Create(handle2)! }!);
        result.Switch(
            success =>
            {
                Assert.AreEqual(handle1, success!.Profiles![0]!.Handle!.ToString());
                Assert.AreEqual(handle2, success!.Profiles[1]!.Handle!.ToString());
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
        var result = await AuthorizedTests.proto.GetProfilesAsync(new List<ATIdentifier?> { test1did, test2did });
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
        var postThreadResult = await AuthorizedTests.proto.GetPostsAsync(new List<ATUri?> { postUri, postUri2 });
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
        var postThreadResult = await AuthorizedTests.proto.GetPostThreadAsync(postUri);
        postThreadResult.Switch(
            success =>
                       {
                           Assert.AreEqual(postUri.ToString(), ((ThreadViewPost)success!.Thread).Post!.Uri!.ToString());
                       },
            failed =>
                        {
                            Assert.Fail($"{failed.StatusCode}: {failed.Detail}");
                        });
    }
}