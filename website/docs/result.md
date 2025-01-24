# `Result` and how to handle it

FishyFlip implements a `Result`-based API for returning results from ATProtocol. Instead of throwing exceptions, calls to `ATProtocol` methods will result in either a `Success` object, with the data requested, or an `ATError` object with the error returned from the system. Handling this can be done in several ways.

**NOTE**: You should still handle exceptions from the library through other methods, such as with try/catch. The `ATError` object is intended for API calls, not general exceptions. If you hit other exceptions, those are most likely bugs within the library and should be filed.

## Switch

```csharp
var postUri = ATUri.Create("at://did:plc:oio4hkxaop4ao4wz2pp3f4cr/app.bsky.feed.post/3lbhrggre422j");
var resultGetPostThreadOutput = await atProtocol.Feed.GetPostThreadAsync(uri: postUri);
// SwitchAsync can also be used to handle Async calls within the result.
resultGetPostThreadOutput.Switch(
success =>
   {
       // Success = GetPostThreadOutput
       // Ex { "thread": { "$type": "app.bsky.feed.defs#threadViewPost", ...
       Console.WriteLine("Success");
       Console.WriteLine(success);
   },
error =>
   {
       // Error = ATError
       // Ex: ATError { StatusCode = 401, Detail = ErrorDetail { Error = AuthMissing, Message = Authentication Required } }
       Console.WriteLine("Error");
       Console.WriteLine(error);
   }
 );
 ```

 ## Deconstruct

 You can also expand entries into their own nullable objects:

 ```csharp
 // Get a raw record from ATProtocol.
 var postUri = ATUri.Create("at://did:plc:oio4hkxaop4ao4wz2pp3f4cr/app.bsky.feed.post/3lbhrggre422j");
 var (success, error) = await atProtocol.Repo.GetRecordAsync(ATIdentifier.Create(postUri.Identity!)!, Post.RecordType, postUri.Rkey);

if (success is GetRecordOutput output)
{
    Console.WriteLine(output.Uri);
}
else if (error != null)
{
    Console.WriteLine(error);
}
```

 ## HandleResult

 You can opt out of the result API by handling the call with `HandleResult()`. This unwraps the result and throws an `ATNetworkErrorException` for errors recieved from ATProtocol.

 ```csharp
var postUri = ATUri.Create("at://did:plc:oio4hkxaop4ao4wz2pp3f4cr/app.bsky.feed.post/3lbhrggre422j");
try
{
    var resultGetPostThreadOutput = (await atProtocol.Feed.GetPostThreadAsync(uri: postUri)).HandleResult();
    Console.WriteLine("Success");
    Console.WriteLine(resultGetPostThreadOutput);
}
catch (ATNetworkErrorException e)
{
    Console.WriteLine("Network Error");
    Console.WriteLine(e);
}
 ``` 

Both of these approaches can be mixed and matched as seen fit.

** NOTE **: In the `Result` object, you will see properties `AsT0`, `AsT1`, `IsT0`, `IsT1`. It is recommended to _NOT_ rely on these properties for handling results. This is a vestage of the `OneOf` library code used to create the Result object. Using these properties will most likely result in harder to read code. If you wish to opt out of the Result API, use `HandleResult` instead.