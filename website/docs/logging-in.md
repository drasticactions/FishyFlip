# Logging In

- There are two methods for logging in: OAuth and App Passwords. App Passwords were the original method for authentication, with OAuth being its replacement. However, the ATProtocol OAuth implementation is still being worked on and not totally final. If building a new application with authentication in mind, you may wish to design with OAuth for the future, but use app passwords today.

- To log in with an App Password, you can call `AuthenticateWithPasswordResultAsync`

```csharp
var atProtocol = new ATProtocolBuilder()
            .WithLogger(new DebugLoggerProvider().CreateLogger("FishyFlip"))
            .Build();

var (session, error) = await atProtocol.AuthenticateWithPasswordResultAsync(identifier, password, cancellationToken);
if (session is null)
{
    Console.WriteLine("Failed to authenticate.");
    return;
}

Console.WriteLine("Authenticated.");
Console.WriteLine($"Session Did: {session.Did}");
Console.WriteLine($"Session Email: {session.Email}");
Console.WriteLine($"Session Handle: {session.Handle}");
Console.WriteLine($"Session Token: {session.AccessJwt}");
```

If you don't override the Instance URL in `ATProtocolBuilder.WithInstanceUrl` the users PDS Host will be resolved before the authentication attempt is made and will be used for authentication and future requests. If you have set it, the authentication request will be resolved against that endpoint. 

- OAuth authentication is more complex. There is a full example showing a [local user authentication session](https://github.com/drasticactions/FishyFlip/tree/develop/samples/OAuth) but in short, you must:
  - Starting the session with `atProtocol.GenerateOAuth2AuthenticationUrlResultAsync`
  - Sending the user to a web browser to log in
  - Handling the callback with the return URI, 
  - Sending that URI to `atProtocol.AuthenticateWithOAuth2CallbackResultAsync` to generate the session.

```csharp
var scopeList = scopes.Split(',').Select(n => n.Trim()).ToArray();
if (scopeList.Length == 0)
{
    consoleLog.LogError("Invalid Scopes");
    return;
}

var atProtocol = this.GenerateProtocol(iUrl);
consoleLog.Log($"Starting OAuth2 Authentication for {instanceUrl}");

// The InstanceUrl should be for where the Users PDS is.
// For example, if they're hosted with the bsky.network domains, it should be bsky.social.
// You can also pass in an ATIdentifier to automatically resolve the PDS url so it can handle the OAuth request.

var (url, error) = await atProtocol.GenerateOAuth2AuthenticationUrlResultAsync(clientId, "http://127.0.0.1", scopeList, instanceUrl.ToString(), cancellationToken);

if (url is null)
{
    consoleLog.LogError("Failed to authenticate, url is null");
    return;
}

consoleLog.Log($"Login URL: {url}");
consoleLog.Log("Please login and copy the URL of the page you are redirected to.");
var redirectUrl = Console.ReadLine();
if (string.IsNullOrEmpty(redirectUrl))
{
    consoleLog.LogError("Invalid redirect URL");
    return;
}

consoleLog.Log($"Got redirect url, finishing OAuth2 Authentication on {instanceUrl}");
var (session, error) = await atProtocol.AuthenticateWithOAuth2CallbackResultAsync(redirectUrl, cancellationToken);

if (session is null)
{
    consoleLog.LogError("Failed to authenticate, session is null");
    return;
}

consoleLog.Log($"Authenticated as {session.Did}");
```
