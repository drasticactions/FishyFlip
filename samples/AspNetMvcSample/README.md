# FishyFlip ASP.NET MVC Sample

This sample demonstrates how to integrate Bluesky authentication using FishyFlip.AspNetCore in an ASP.NET Core MVC application with password-based authentication.

## Features

- **Password-based Authentication**: Login with your Bluesky handle/email and app password
- **Session Management**: Secure session storage and automatic token refresh
- **Profile View**: Display user profile information from Bluesky
- **Timeline View**: Show the user's Bluesky timeline
- **Cookie Authentication**: Standard ASP.NET Core cookie authentication flow

## Prerequisites

- .NET 9.0 SDK
- A Bluesky account
- App password (created at [bsky.app/settings/app-passwords](https://bsky.app/settings/app-passwords))

## Running the Sample

1. Clone the repository and navigate to the sample directory:
   ```bash
   cd samples/AspNetMvcSample
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and navigate to `https://localhost:5001`

4. Click "Login" and enter your Bluesky credentials:
   - **Handle or Email**: Your Bluesky handle (e.g., `alice.bsky.social`) or email
   - **App Password**: Create one at [bsky.app/settings/app-passwords](https://bsky.app/settings/app-passwords)
   - **Server**: Leave as `https://bsky.social` unless using a custom PDS

## Key Components

### Program.cs Configuration

```csharp
// Add FishyFlip services
builder.Services.AddFishyFlip(options =>
{
    options.InstanceUrl = "https://bsky.social";
    options.SessionExpiration = TimeSpan.FromHours(24);
    options.AutoRefreshTokens = true;
});

// Add cookie authentication
builder.Services.AddFishyFlipCookieAuthentication(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});
```

### Controllers

- **HomeController**: Displays public home page, authenticated profile, and timeline
- **AccountController**: Handles login/logout functionality

### Authentication Flow

1. User submits login form with Bluesky credentials
2. `IUserSessionManager.CreatePasswordSessionAsync()` creates a session
3. ASP.NET Core cookie authentication stores user claims
4. Subsequent requests use the session to access Bluesky APIs
5. Tokens are automatically refreshed when needed

### Session Management

The sample uses the default `InMemorySessionStore` for storing user sessions. In production, you should implement a persistent session store (Redis, SQL Server, etc.).

## Security Notes

- **Use App Passwords**: Never use your main account password. Create app passwords at [bsky.app/settings/app-passwords](https://bsky.app/settings/app-passwords)
- **HTTPS Only**: Always use HTTPS in production
- **Session Storage**: Implement a persistent session store for production use
- **Token Security**: Sessions are automatically cleaned up and tokens refreshed

## Customization

### Custom Session Store

Implement `ISessionStore` for persistent session storage:

```csharp
public class SqlServerSessionStore : ISessionStore
{
    // Implement session storage in SQL Server
}

// Register in Program.cs
builder.Services.AddFishyFlip<SqlServerSessionStore>();
```

### Custom Instance URL

To connect to a different AT Protocol server:

```csharp
builder.Services.AddFishyFlip(options =>
{
    options.InstanceUrl = "https://your-custom-pds.example.com";
});
```

## Learning More

- [FishyFlip Documentation](https://github.com/drasticactions/FishyFlip)
- [AT Protocol Specification](https://atproto.com/)