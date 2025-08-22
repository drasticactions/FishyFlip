// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.Threading;
using BSkyOAuth;
using FishyFlip.Lexicon.Com.Atproto.Lexicon;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Activation;

namespace BSkyOAuth;

/// <summary>
/// Main Program.
/// </summary>
internal class Program
{
    private const string AppKey = "2317741F-805D-4586-999A-9F971DFE1397";

    [STAThread]
    private static int Main(string[] args)
    {
        WinRT.ComWrappersSupport.InitializeComWrappers();
        bool isRedirect = DecideRedirection();
        if (!isRedirect)
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                Microsoft.Windows.AppLifecycle.ActivationRegistrationManager.RegisterForProtocolActivation("vip.drasticactions", null, "BSkyOauthApp", Environment.ProcessPath);

                var context = new DispatcherQueueSynchronizationContext(
                    DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }

        return 0;
    }

    private static bool DecideRedirection()
    {
        bool isRedirect = false;
        AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
        ExtendedActivationKind kind = args.Kind;
        AppInstance keyInstance = AppInstance.FindOrRegisterForKey(AppKey);

        if (keyInstance.IsCurrent)
        {
            keyInstance.Activated += OnActivated;
        }
        else
        {
            isRedirect = true;
            keyInstance.RedirectActivationToAsync(args).GetAwaiter().GetResult();
        }

        return isRedirect;
    }

    private static void OnActivated(object? sender, AppActivationArguments appActivationArguments)
    {
        ExtendedActivationKind kind = appActivationArguments.Kind;
        if (appActivationArguments.Kind == ExtendedActivationKind.Protocol && appActivationArguments.Data is IProtocolActivatedEventArgs protocolArgs)
        {
            var uriComponents = protocolArgs.Uri;
            if (App.MainWindow is MainWindow mainWindow)
            {
                mainWindow.DispatcherQueue.TryEnqueue(async () => await mainWindow.HandleOAuthCallAsync(uriComponents));
            }
        }
    }
}
