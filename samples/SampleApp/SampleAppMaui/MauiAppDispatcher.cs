// <copyright file="MauiAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drastic.Services;

namespace SampleAppMaui;

public class MauiAppDispatcher : IAppDispatcher
{
    /// <inheritdoc/>
    public bool Dispatch(Action action)
    {
        return Microsoft.Maui.Controls.Application.Current!.Dispatcher.Dispatch(action);
    }
}
