# FishyFlip ASP.NET OAuth Sample

This sample demonstrates how to integrate Bluesky OAuth 2.0 authentication using FishyFlip.AspNetCore in an ASP.NET Core MVC application.

## Features

- **OAuth 2.0 Authentication**: Secure authentication using industry-standard OAuth 2.0 flow
- **PKCE Support**: Proof Key for Code Exchange for enhanced security
- **Session Management**: Automatic session storage and token refresh
- **Multi-Tenant Ready**: Support for multiple concurrent users
- **Full Bluesky Integration**: Profile viewing, timeline display, and post creation
- **Responsive Design**: Modern UI that works on desktop and mobile

## Prerequisites

- .NET 9.0 SDK
- A Bluesky account
- OAuth client registration with Bluesky (see setup instructions below)

## OAuth Client Setup

Before running this sample, you need to register your OAuth client with Bluesky:

### 1. Create OAuth Client Metadata

You'll need to host a client metadata endpoint. The simplest way is to use the included metadata sample:

```bash
cd samples/BSkyOauth/BSkyOAuth.ClientMetadata
dotnet run
```

This will host your client metadata at `http://localhost:5000/client-metadata.json`.

### 2. Register Your Client

Currently, OAuth client registration with Bluesky requires manual setup. You'll need:

- **Client ID**: A unique identifier for your application
- **Client Metadata URI**: URL where Bluesky can fetch your client metadata
- **Redirect URIs**: Authorized callback URLs for your application

### 3. Configure the Application

Update the configuration in `Program.cs` or use environment variables:

```csharp
builder.Services.AddFishyFlip(options =>
{
    options.ClientId = "your-oauth-client-id";
    options.RedirectUri = "https://localhost:5001/Account/OAuthCallback";
    options.InstanceUrl = "https://bsky.social";
});
```

Or set environment variables:
```bash
export Bluesky__ClientId="your-oauth-client-id"
export Bluesky__RedirectUri="https://localhost:5001/Account/OAuthCallback"
```

## Running the Sample

1. Navigate to the sample directory:
   ```bash
   cd samples/AspNetOAuthSample
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and navigate to `https://localhost:5001`

4. Click "Sign In with Bluesky" to start the OAuth flow

## OAuth Flow Walkthrough

### 1. **Initiate Authentication**
- User clicks "Sign In with Bluesky"
- Application generates OAuth state and PKCE parameters
- User is redirected to Bluesky's authorization server

### 2. **User Authorization**
- User logs into Bluesky (if not already logged in)
- User reviews and grants permissions to your application
- Bluesky redirects back to your application with authorization code

### 3. **Complete Authentication**
- Application exchanges authorization code for access tokens
- Session is created and stored securely
- User is authenticated and redirected to requested page

### 4. **API Access**
- Authenticated users can access their Bluesky data
- Tokens are automatically refreshed before expiration
- Session persists across browser sessions

## Key Components

### Program.cs Configuration

```csharp
// Configure FishyFlip with OAuth
builder.Services.AddFishyFlip(options =>
{
    options.ClientId = "your-oauth-client-id";
    options.RedirectUri = "https://localhost:5001/Account/OAuthCallback";
    options.Scopes = new[] { "atproto", "transition:generic" };
});

// Add cookie authentication for session management
builder.Services.AddFishyFlipCookieAuthentication();
```

### OAuth Flow Controller

The `AccountController` handles:
- **`/Account/Login`**: Shows login page and initiates OAuth
- **`/Account/StartOAuth`**: Starts the OAuth authorization flow
- **`/Account/OAuthCallback`**: Handles OAuth callback and completes authentication
- **`/Account/Logout`**: Logs out user and cleans up session

### Session Management

- Sessions are stored using `ISessionStore` (default: in-memory)
- Automatic token refresh before expiration
- Secure cookie-based authentication
- Multi-tenant session isolation

## Security Features

- **CSRF Protection**: OAuth state parameter prevents cross-site request forgery
- **PKCE**: Proof Key for Code Exchange protects against authorization code interception
- **Secure Cookies**: HttpOnly, Secure, and SameSite cookie attributes
- **No Password Storage**: User passwords never touch your server
- **Token Encryption**: Sessions stored securely with automatic cleanup

## API Integration Examples

### Get User Profile
```csharp
[Authorize]
public async Task<IActionResult> Profile()
{
    var atProtocol = await HttpContext.GetUserATProtocolAsync();
    var did = HttpContext.GetBlueskyDid();
    var profile = await atProtocol.Actor.GetProfileAsync(ATDid.Create(did)!);
    return View(profile);
}
```

### Create a Post
```csharp
[Authorize]
public async Task<IActionResult> CreatePost(string postText)
{
    var atProtocol = await HttpContext.GetUserATProtocolAsync();
    var result = await atProtocol.Feed.CreatePostAsync(postText);
    return RedirectToAction("Timeline");
}
```

## Customization

### Custom Session Store

For production use, implement `ISessionStore` for persistent storage:

```csharp
public class RedisSessionStore : ISessionStore
{
    // Implement Redis-based session storage
}

// Register in Program.cs
builder.Services.AddFishyFlip<RedisSessionStore>();
```

### Custom OAuth Scopes

Configure different scopes based on your application needs:

```csharp
builder.Services.AddFishyFlip(options =>
{
    options.Scopes = new[] { "atproto", "transition:generic", "custom:scope" };
});
```

## Production Considerations

- **OAuth Client Registration**: Complete formal client registration with Bluesky
- **HTTPS Only**: Always use HTTPS in production environments
- **Session Storage**: Use persistent storage (Redis, SQL Server) instead of in-memory
- **Error Handling**: Implement comprehensive error handling and logging
- **Rate Limiting**: Respect Bluesky's API rate limits
- **Token Security**: Consider additional encryption for stored tokens

## Troubleshooting

### Common Issues

1. **"OAuth not properly configured"**: Verify `ClientId` and `RedirectUri` are set correctly
2. **"Invalid or expired OAuth state"**: Check that session storage is working properly
3. **"Failed to complete OAuth flow"**: Verify redirect URI matches exactly with registered URI

### Debug Logging

Enable detailed logging to troubleshoot OAuth issues:

```csharp
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);
```

## Learning More

- [FishyFlip Documentation](https://github.com/drasticactions/FishyFlip)
- [AT Protocol OAuth Specification](https://atproto.com/specs/oauth)
- [Bluesky Developer Guidelines](https://bsky.social/about/blog/7-05-2023-developer-guidelines)
- [OAuth 2.0 Security Best Practices](https://tools.ietf.org/html/draft-ietf-oauth-security-topics)