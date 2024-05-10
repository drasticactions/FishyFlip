using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.ViewModels;

namespace SampleAppMaui;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(IServiceProvider provider)
    {
        InitializeComponent();
        this.BindingContext = this.ViewModel = provider.GetRequiredService<ProfileViewModel>();
    }
    
    public ProfileViewModel ViewModel { get; }
}