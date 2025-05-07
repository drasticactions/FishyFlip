# FishyFlip.Xrpc - a .NET ATProtocol/Bluesky Library for XRPC endpoints

[![NuGet Version](https://img.shields.io/nuget/v/FishyFlip.Xrpc.svg)](https://www.nuget.org/packages/FishyFlip.Xrpc/) ![License](https://img.shields.io/badge/License-MIT-blue.svg)

![FishyFlip.Xrpc Logo](https://user-images.githubusercontent.com/898335/253740405-4b0ae177-cc49-4c26-b6b0-ab8e835a0e62.png)

FishyFlip.Xrpc is an experimental implmentation of the [ATProtocol XRPC specification](https://atproto.com/specs/xrpc) through source generation. It maps the XRPC entries listed in Bluesky and other ATProtocol lexicons to ASP.NET MVC controllers. As it binds to FishyFlip's existing classes, it can handle model and parameter validation, and being source generated along with FishyFlip can map to the latest changes in the lexicons whenever a value changes. This should make it easier to keep up with the upstream sources.  

As an example

```csharp
public class FFBlogController : FishyFlip.Xrpc.Lexicon.Com.Whtwnd.Blog.BlogController
{
    /// <inheritdoc/>
    public async override Task<Results<Ok<GetAuthorPostsOutput>, ATErrorResult>> GetAuthorPostsAsync(ATDid author, CancellationToken cancellationToken = default)
    {
        return ATErrorResult.NotFound();
    }

    /// <inheritdoc/>
    public async override Task<Results<Ok<GetEntryMetadataByNameOutput>, ATErrorResult>> GetEntryMetadataByNameAsync([FromQuery] ATIdentifier author, [FromQuery] string entryTitle, CancellationToken cancellationToken = default)
    {
        return ATErrorResult.BadRequest();
    }

    public async override Task<Results<Ok<GetMentionsByEntryOutput>, ATErrorResult>> GetMentionsByEntryAsync([FromQuery] ATUri postUri, CancellationToken cancellationToken = default)
    {
        return ATErrorResult.InternalServerError();
    }

    public async override Task<Results<Ok<NotifyOfNewEntryOutput>, ATErrorResult>> NotifyOfNewEntryAsync([FromBody] ATUri entryUri, CancellationToken cancellationToken = default)
    {
        var result = new NotifyOfNewEntryOutput();
        return TypedResults.Ok(result);
    }
}
```

This implmentation maps to the [WhtWnd Lexicon](https://github.com/whtwnd/whitewind-blog/tree/main/lexicons) XRPC endpoints, and can either return the object expected by the lexicon, or an error object that maps to the XRPC error methods.

## ATErrorResult

ATErrorResult is an `IResult` that maps to the listed documented expected error results, with a JSON object in a format that's expected. There is no direct list of expected response Error messages and detail messages, but the format should at least map to what Bluesky itself uses.