// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Configure FishyFlip with OAuth settings
builder.Services.AddFishyFlip(options =>
{
    // OAuth Configuration - Replace with your actual OAuth client details
    options.ClientId = builder.Configuration["Bluesky:ClientId"] ?? "http://localhost";
    options.RedirectUri = builder.Configuration["Bluesky:RedirectUri"] ?? "http://127.0.0.1:5000/auth/bluesky/oauth/callback";
    options.InstanceUrl = builder.Configuration["Bluesky:InstanceUrl"] ?? "https://bsky.social"; // Default Bluesky instance
    options.Scopes = new[] { "atproto" };
    options.SessionExpiration = TimeSpan.FromHours(24);
    options.AutoRefreshTokens = true;
});

// Add cookie authentication for OAuth flow
builder.Services.AddFishyFlipCookieAuthentication(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
    options.Cookie.Name = "FishyFlip.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map the FishyFlip authentication endpoints
app.MapControllers();

app.Run();