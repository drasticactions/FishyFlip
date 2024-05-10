// <copyright file="FirehoseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Drastic.Tools;
using Drastic.ViewModels;
using FishyFlip;
using FishyFlip.Events;
using FishyFlip.Models;

namespace SampleApp.ViewModels;

public class FirehoseViewModel : BaseViewModel
{
#if DEBUG
    public static FirehoseViewModel DesignInstance = new(null!);
#endif

    private ATProtocol protocol;
    private ATWebSocketProtocol webSocketProtocol;
    private AsyncCommand connectCommand;
    private AsyncCommand stopCommand;
    private AsyncCommand cleanCommand;

    public FirehoseViewModel(IServiceProvider services)
        : base(services)
    {
        var protocolBuilder = new ATProtocolBuilder();
        this.protocol = protocolBuilder.Build();
        var webProtocolBuilder = new ATWebSocketProtocolBuilder();
        this.webSocketProtocol = webProtocolBuilder.Build();
        this.webSocketProtocol.OnRecordReceived += this.WebSocketProtocol_OnRecordReceived;
        this.connectCommand = new AsyncCommand(this.ConnectAsync, () => !this.webSocketProtocol.IsConnected, this.Dispatcher, this.ErrorHandler);
        this.stopCommand = new AsyncCommand(this.StopAsync, () => this.webSocketProtocol.IsConnected, this.Dispatcher, this.ErrorHandler);
        this.cleanCommand = new AsyncCommand(this.CleanAsync, null, this.Dispatcher, this.ErrorHandler);
    }

    public AsyncCommand ConnectCommand => this.connectCommand;

    public AsyncCommand StopCommand => this.stopCommand;

    public AsyncCommand CleanCommand => this.cleanCommand;

    public ObservableCollection<ATRecord> Records { get; } = new ObservableCollection<ATRecord>();

    /// <inheritdoc/>
    public override void RaiseCanExecuteChanged()
    {
        base.RaiseCanExecuteChanged();
        this.connectCommand.RaiseCanExecuteChanged();
        this.stopCommand.RaiseCanExecuteChanged();
    }

    private async Task ConnectAsync()
    {
        await this.webSocketProtocol.StartSubscribeReposAsync();
        this.RaiseCanExecuteChanged();
    }

    private async Task StopAsync()
    {
        await this.webSocketProtocol.StopSubscriptionAsync();
        this.RaiseCanExecuteChanged();
    }

    private Task CleanAsync()
    {
        lock (this)
        {
            this.Records.Clear();
        }

        this.RaiseCanExecuteChanged();
        return Task.CompletedTask;
    }

    private void WebSocketProtocol_OnRecordReceived(object? sender, RecordMessageReceivedEventArgs e)
    {
        if (e.Record != null)
        {
            lock (this)
            {
                this.Records.Add(e.Record);
            }
        }
    }
}
