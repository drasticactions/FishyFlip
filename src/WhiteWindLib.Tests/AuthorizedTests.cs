using Microsoft.Extensions.Logging.Debug;

namespace WhiteWindLib.Tests;

[TestClass]
public class AuthorizedTests
{
    static ATProtocol proto;
    static WhiteWindBlog blog;
    static string handle;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        handle = (string?)context.Properties["BLUESKY_TEST_HANDLE"] ?? throw new ArgumentNullException();
        string password = (string?)context.Properties["BLUESKY_TEST_PASSWORD"] ?? throw new ArgumentNullException();
        string instance = (string?)context.Properties["BLUESKY_INSTANCE_URL"] ?? throw new ArgumentNullException();
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(false)
            .WithInstanceUrl(new Uri(instance))
            .WithLogger(debugLog.CreateLogger("FishyFlipTests"));
        AuthorizedTests.proto = atProtocolBuilder.Build();
        AuthorizedTests.proto.Server.CreateSessionAsync(AuthorizedTests.handle, password).Wait();
        AuthorizedTests.blog = new WhiteWindBlog(AuthorizedTests.proto);
    }

    [TestMethod]
    public async Task HandleCreateAndRemoveEntry()
    {
        var (result, error) = await AuthorizedTests.blog.CreateEntryAsync("Unit Test Content", "Unit Test Title", visibility: "author");
        Assert.IsNull(error);
        Assert.IsNotNull(result);
        var (result2, error2) = await AuthorizedTests.blog.GetEntryAsync(result.Uri!.Did!, result.Uri.Rkey);
        Assert.IsNull(error2);
        Assert.IsNotNull(result2);
        Assert.AreEqual("Unit Test Content", result2.Value!.Content);
        Assert.AreEqual("Unit Test Title", result2.Value!.Title);
        var (result3, error3) = await AuthorizedTests.blog.DeleteEntryAsync(result.Uri.Did!, result.Uri.Rkey);
        Assert.IsNull(error3);
        Assert.IsNotNull(result3);
    }
}