using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Services;
using FishyFlip;
using FishyFlipMaui.ViewModels;
using FishyFlipWinUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FishyFlipWinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            var debugLog = new DebugLoggerProvider();
            //var atProtocolBuilder = new ATProtocolBuilder()
            //    .EnableAutoRenewSession(true)
            //    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
            var atProtocolBuilder = new ATProtocolBuilder()
                .EnableAutoRenewSession(true);
            var atProtocol = atProtocolBuilder.Build();
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            string logLocation = WinUIExtensions.IsRunningAsUwp() ? System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "fishyflip-error.txt") : System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location!)!, "fishyflip-error.txt");
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton(atProtocol)
                .AddSingleton<IErrorHandlerService>(new WinUIErrorHandlerService(logLocation))
                .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
                .AddSingleton<FirehoseViewModel>()
                .BuildServiceProvider());
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}

/// <summary>
/// WinUI Extensions.
/// </summary>
public static class WinUIExtensions
{
    /// <summary>
    /// Small Icon.
    /// </summary>
    private const int ICONSMALL = 0;

    /// <summary>
    /// Big Icon.
    /// </summary>
    private const int ICONBIG = 1;

    /// <summary>
    /// Icon Small 2.
    /// </summary>
    private const int ICONSMALL2 = 2;

    /// <summary>
    /// Get Icon.
    /// </summary>
    private const int WMGETICON = 0x007F;

    /// <summary>
    /// Set Icon.
    /// </summary>
    private const int WMSETICON = 0x0080;

    private const long APPMODELERRORNOPACKAGE = 15700L;

    /// <summary>
    /// Is the app running as a UWP.
    /// </summary>
    /// <returns>bool.</returns>
    public static bool IsRunningAsUwp()
    {
        int length = 0;
        StringBuilder sb = new StringBuilder(0);
        int result = GetCurrentPackageFullName(ref length, sb);

        sb = new StringBuilder(length);
        result = GetCurrentPackageFullName(ref length, sb);

        return result != APPMODELERRORNOPACKAGE;
    }

    /// <summary>
    /// Get the current version of app. Returns the store version if UWP. Returns the assembly version if unpackaged.
    /// </summary>
    /// <returns>String.</returns>
    public static string GetAppVersion()
    {
        if (IsRunningAsUwp())
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}-Store", version.Major, version.Minor, version.Build, version.Revision);
        }

        return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Missing";
    }

    /// <summary>
    /// Create a random access stream from a byte array.
    /// </summary>
    /// <param name="array">The byte array.</param>
    /// <returns><see cref="IRandomAccessStream"/>.</returns>
    public static IRandomAccessStream ToRandomAccessStream(this byte[] array)
    {
        InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
        using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
        {
            writer.WriteBytes(array);
            writer.StoreAsync().GetResults();
        }

        return ms;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);

    /// <summary>
    /// Send Message to App.
    /// </summary>
    /// <param name="hWnd">Pointer.</param>
    /// <param name="msg">Message.</param>
    /// <param name="wParam">W Parameter.</param>
    /// <param name="lParam">L Parameter.</param>
    /// <returns>Int.</returns>
    [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);
}