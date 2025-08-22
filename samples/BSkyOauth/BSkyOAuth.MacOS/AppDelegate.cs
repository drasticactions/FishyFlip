// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace BSkyOAuth;

[Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate
{
    private MainWindow? window;

    public override void DidFinishLaunching(NSNotification notification)
    {
        this.window = new MainWindow();
        this.window.MakeKeyAndOrderFront(this);
        this.window.Center();
    }

    public override void WillTerminate(NSNotification notification)
    {
        // Insert code here to tear down your application
    }
}
