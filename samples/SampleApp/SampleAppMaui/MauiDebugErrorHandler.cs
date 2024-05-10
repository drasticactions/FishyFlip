// <copyright file="MauiDebugErrorHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace SampleAppMaui;

public class MauiDebugErrorHandler : IErrorHandlerService
{
    public void HandleError(Exception ex)
    {
#if DEBUG
        System.Diagnostics.Debugger.Break();
        System.Diagnostics.Debug.WriteLine(ex);
#else
        // Log the error.
        System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
    }
}
