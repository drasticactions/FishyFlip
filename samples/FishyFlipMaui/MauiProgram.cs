// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;
using FishyFlip;
using FishyFlipMaui.Services;
using FishyFlipMaui.ViewModels;
#if !WINDOWS && !ANDROID
using Maui.Nuke;
#endif
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace FishyFlipMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var debugLog = new DebugLoggerProvider();
        var atProtocolBuilder = new ATProtocolBuilder()
            .EnableAutoRenewSession(true)
            .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
        var atProtocol = atProtocolBuilder.Build();
        var builder = MauiApp.CreateBuilder();

        builder
            .Services
            .AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddProvider(debugLog);
            })
            .AddSingleton<IAppDispatcher, MauiAppDispatcher>()
            .AddSingleton<IErrorHandlerService, MauiErrorHandler>()
            .AddSingleton<ATProtocol>(atProtocol)
            .AddSingleton<FirehoseViewModel>();

        builder
            .UseMauiApp<App>()
#if !WINDOWS && !ANDROID
            .UseNuke(showDebugLogs: false)
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
