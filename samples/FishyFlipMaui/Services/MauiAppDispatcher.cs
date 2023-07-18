// <copyright file="MauiAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace FishyFlipMaui.Services;

public class MauiAppDispatcher : IAppDispatcher
{
    public bool Dispatch(Action action)
    {
       return Microsoft.Maui.Controls.Application.Current.Dispatcher.Dispatch(action);
    }
}
