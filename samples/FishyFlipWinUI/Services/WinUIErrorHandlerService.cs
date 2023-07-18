// <copyright file="WinUIErrorHandlerService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace FishyFlipWinUI.Services;

/// <summary>
/// WinUI Error handler Service.
/// </summary>
public class WinUIErrorHandlerService : IErrorHandlerService, IDisposable
{
    private bool disposedValue;
    private StreamWriter writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="WinUIErrorHandlerService"/> class.
    /// </summary>
    /// <param name="logLocation">Location of logs.</param>
    public WinUIErrorHandlerService(string logLocation)
    {
        this.writer = new StreamWriter(logLocation);
        this.writer.AutoFlush = true;
    }

    /// <inheritdoc/>
    public void HandleError(Exception ex)
    {
        this.writer.WriteLine($"({DateTime.UtcNow}): {ex}");
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Is Disposing.
    /// </summary>
    /// <param name="disposing">Disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.writer.Close();
                this.writer.Dispose();
            }

            this.disposedValue = true;
        }
    }
}
