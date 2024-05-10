namespace SampleAppMaui;

public class AppShell : Shell
{
    private readonly IServiceProvider _provider;
    
    public AppShell(IServiceProvider provider)
    {
        this.Items.Add(new ShellContent() { Content = new FirehosePage(provider), Title = "Firehose Collection View" });
        this.Items.Add(new ShellContent() { Content = new ProfilePage(provider), Title = "Profile Page" });
        this.FlyoutBehavior = FlyoutBehavior.Flyout;
    }
}