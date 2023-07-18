// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlipMaui.ViewModels;

namespace FishyFlipMaui;

public partial class MainPage : ContentPage
{
    private FirehoseViewModel vm;

    public MainPage()
    {
        this.InitializeComponent();
        this.BindingContext = this.vm = Microsoft.Maui.Controls.Application.Current.Handler.MauiContext.Services.GetRequiredService<FirehoseViewModel>();
    }
}
