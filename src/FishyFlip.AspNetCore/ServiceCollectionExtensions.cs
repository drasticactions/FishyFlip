// <copyright file="ServiceCollectionExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Authentication;
using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace FishyFlip.AspNetCore;

/// <summary>
/// Extension methods for configuring FishyFlip services in ASP.NET Core.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds FishyFlip services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure FishyFlip options.</param>
    /// <param name="configurationSection">Optional configuration section to bind configuration to.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddFishyFlip(this IServiceCollection services, Action<FishyFlipOptions>? configureOptions = null, string? configurationSection = null)
    {
        return services.AddFishyFlip<InMemorySessionStore>(configureOptions, configurationSection);
    }

    /// <summary>
    /// Adds FishyFlip services with a custom session store implementation.
    /// </summary>
    /// <typeparam name="TSessionStore">The session store implementation type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure FishyFlip options.</param>
    /// <param name="configurationSection">Optional configuration section to bind options to.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddFishyFlip<TSessionStore>(this IServiceCollection services, Action<FishyFlipOptions>? configureOptions = null, string? configurationSection = null)
        where TSessionStore : class, ISessionStore
    {
        services.ConfigureFishyFlip(configureOptions);
        services.AddSingleton<FishyFlipOptions>(serviceProvider => serviceProvider.GetRequiredService<IOptions<FishyFlipOptions>>().Value);
        services.AddMemoryCache();
        services.AddScoped<ISessionStore, TSessionStore>();
        services.AddScoped<IUserSessionManager, UserSessionManager>();
        services.AddScoped<IOAuthFlowManager, OAuthFlowManager>();
        return services;
    }

    /// <summary>
    /// Adds FishyFlip cookie authentication to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure cookie authentication options.</param>
    /// <param name="authenticationScheme">Sets the authentication scheme to use for cookie authentication.</param>
    /// <returns>The authentication builder for further configuration.</returns>
    public static AuthenticationBuilder AddFishyFlipCookieAuthentication(this IServiceCollection services, Action<CookieAuthenticationOptions>? configureOptions = null, string authenticationScheme = FishyFlipConfigurationDefaults.DefaultCookieAuthenticationScheme)
    {
        return services.AddAuthentication(authenticationScheme)
            .AddScheme<BlueskyAuthenticationSchemeOptions, BlueskyAuthenticationHandler>(
                authenticationScheme,
                _ => { })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions!);
    }

    /// <summary>
    /// Adds FishyFlip JWT bearer authentication to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure JWT bearer options.</param>
    /// <param name="authenticationScheme">Authentication scheme to use for JWT Bearer authentication.</param>
    /// <returns>The authentication builder for further configuration.</returns>
    public static AuthenticationBuilder AddFishyFlipJwtBearerAuthentication(this IServiceCollection services, Action<JwtBearerOptions>? configureOptions = null, string authenticationScheme = FishyFlipConfigurationDefaults.DefaultJwtBearerAuthenticationScheme)
    {
        return services.AddAuthentication(authenticationScheme)
            .AddScheme<BlueskyJwtBearerSchemeOptions, BlueskyJwtBearerAuthenticationHandler>(
                authenticationScheme,
                _ => { })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions!);
    }

    /// <summary>
    /// Adds both FishyFlip cookie and JWT bearer authentication to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureCookieOptions">Action to configure cookie authentication options.</param>
    /// <param name="configureJwtOptions">Action to configure JWT bearer options.</param>
    /// <param name="cookieAuthenticationScheme">Cookie authentication scheme.</param>
    /// <param name="jwtBearerAuthenticationScheme">JWT Bearer authentication scheme.</param>
    /// <returns>The authentication builder for further configuration.</returns>
    public static AuthenticationBuilder AddFishyFlipAuthentication(
        this IServiceCollection services,
        Action<CookieAuthenticationOptions>? configureCookieOptions = null,
        Action<JwtBearerOptions>? configureJwtOptions = null,
        string cookieAuthenticationScheme = FishyFlipConfigurationDefaults.DefaultCookieAuthenticationScheme,
        string jwtBearerAuthenticationScheme = FishyFlipConfigurationDefaults.DefaultJwtBearerAuthenticationScheme)
    {
        return services.AddAuthentication()
            .AddScheme<BlueskyAuthenticationSchemeOptions, BlueskyAuthenticationHandler>(
                cookieAuthenticationScheme,
                options => { })
            .AddScheme<BlueskyJwtBearerSchemeOptions, BlueskyJwtBearerAuthenticationHandler>(
                jwtBearerAuthenticationScheme,
                options => { })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureCookieOptions!)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureJwtOptions!);
    }

    private static void ConfigureFishyFlip(
        this IServiceCollection services,
        Action<FishyFlipOptions>? configureOptions = null,
        string? configurationSectionName = null)
    {
        var optionsBuilder = services.AddOptions<FishyFlipOptions>();
        if (configureOptions != null)
        {
            optionsBuilder.Configure(configureOptions);
        }

        if (!string.IsNullOrWhiteSpace(configurationSectionName))
        {
            optionsBuilder.BindConfiguration(configurationSectionName);
        }
    }
}