using SampleApp.ViewModels;

namespace SampleAppMaui;

public partial class FirehosePage : ContentPage
{
    public FirehosePage(IServiceProvider provider)
    {
        InitializeComponent();
        this.BindingContext = this.ViewModel = provider.GetRequiredService<FirehoseViewModel>();
    }

    public FirehoseViewModel ViewModel { get; }
}