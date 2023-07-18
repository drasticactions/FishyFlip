// <copyright file="FirehosePage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using FishyFlipMaui.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace FishyFlipWinUI
{
    public sealed partial class FirehosePage : Page
    {
        private FirehoseViewModel vm;

        public FirehosePage()
        {
            this.InitializeComponent();
            this.DataContext = this.vm = Ioc.Default.GetRequiredService<FirehoseViewModel>();
        }

        public FirehoseViewModel ViewModel => this.vm;
    }
}
