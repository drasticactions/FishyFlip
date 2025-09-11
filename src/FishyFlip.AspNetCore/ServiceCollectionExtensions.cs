// <copyright file="ServiceCollectionExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Authentication;
using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddFishyFlip(this IServiceCollection services, Action<FishyFlipOptions>? configureOptions = null)
    {
        var options = new FishyFlipOptions();
        configureOptions?.Invoke(options);

        services.AddSingleton(options);
        services.AddMemoryCache();
        services.TryAddScoped<ISessionStore, InMemorySessionStore>();
        services.AddScoped<IUserSessionManager, UserSessionManager>();
        services.AddScoped<IOAuthFlowManager, OAuthFlowManager>();

        return services;
    }

    /// <summary>
    /// Adds FishyFlip services with a custom session store implementation.
    /// </summary>
    /// <typeparam name="TSessionStore">The session store implementation type.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure FishyFlip options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddFishyFlip<TSessionStore>(this IServiceCollection services, Action<FishyFlipOptions>? configureOptions = null)
        where TSessionStore : class, ISessionStore
    {
        services.AddFishyFlip(configureOptions);
        services.Replace(ServiceDescriptor.Scoped<ISessionStore, TSessionStore>());
        return services;
    }

    /// <summary>
    /// Adds FishyFlip cookie authentication to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure cookie authentication options.</param>
    /// <returns>The authentication builder for further configuration.</returns>
    public static AuthenticationBuilder AddFishyFlipCookieAuthentication(this IServiceCollection services, Action<CookieAuthenticationOptions>? configureOptions = null)
    {
        var fishyFlipOptions = services.BuildServiceProvider().GetService<FishyFlipOptions>() ?? new FishyFlipOptions();

        return services.AddAuthentication(fishyFlipOptions.CookieAuthenticationScheme)
            .AddScheme<BlueskyAuthenticationSchemeOptions, BlueskyAuthenticationHandler>(
                fishyFlipOptions.CookieAuthenticationScheme,
                options => { })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions!);
    }

    /// <summary>
    /// Adds FishyFlip JWT bearer authentication to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure JWT bearer options.</param>
    /// <returns>The authentication builder for further configuration.</returns>
    public static AuthenticationBuilder AddFishyFlipJwtBearerAuthentication(this IServiceCollection services, Action<JwtBearerOptions>? configureOptions = null)
    {
        var fishyFlipOptions = services.BuildServiceProvider().GetService<FishyFlipOptions>() ?? new FishyFlipOptions();

        return services.AddAuthentication(fishyFlipOptions.JwtBearerAuthenticationScheme)
            .AddScheme<BlueskyJwtBearerSchemeOptions, BlueskyJwtBearerAuthenticationHandler>(
                fishyFlipOptions.JwtBearerAuthenticationScheme,
                options => { })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions!);
    }

    /// <summary>
    /// Adds both FishyFlip cookie and JWT bearer authentication to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureCookieOptions">Action to configure cookie authentication options.</param>
    /// <param name="configureJwtOptions">Action to configure JWT bearer options.</param>
    /// <returns>The authentication builder for further configuration.</returns>
    public static AuthenticationBuilder AddFishyFlipAuthentication(
        this IServiceCollection services,
        Action<CookieAuthenticationOptions>? configureCookieOptions = null,
        Action<JwtBearerOptions>? configureJwtOptions = null)
    {
        var fishyFlipOptions = services.BuildServiceProvider().GetService<FishyFlipOptions>() ?? new FishyFlipOptions();

        return services.AddAuthentication()
            .AddScheme<BlueskyAuthenticationSchemeOptions, BlueskyAuthenticationHandler>(
                fishyFlipOptions.CookieAuthenticationScheme,
                options => { })
            .AddScheme<BlueskyJwtBearerSchemeOptions, BlueskyJwtBearerAuthenticationHandler>(
                fishyFlipOptions.JwtBearerAuthenticationScheme,
                options => { })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureCookieOptions!)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureJwtOptions!);
    }
}