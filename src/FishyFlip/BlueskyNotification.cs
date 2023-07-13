// <copyright file="BlueskyNotification.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;
using FishyFlip.Tools;

namespace FishyFlip;

public sealed class BlueskyNotification
{
    private ATProtocol proto;

    internal BlueskyNotification(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    public Task<Result<UnreadCount?>> GetUnreadCountAsync(DateTime? seenAt = null, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Notification.NotificationGetUnreadCount;
        if (seenAt is not null)
        {
            url += $"?seenAt={seenAt.Value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)}";
        }

        return this.Client.Get<UnreadCount>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    public Task<Result<Success?>> UpdateSeenAsync(DateTime seenAt, CancellationToken cancellationToken = default)
    {
        var createSeenAtRecord = new CreateSeenAtRecord(seenAt.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
        var url = Constants.Urls.Bluesky.Notification.NotificationUpdateSeen;

        return this.Client.Post<CreateSeenAtRecord, Success?>(url, this.Options.JsonSerializerOptions, createSeenAtRecord, cancellationToken, this.Options.Logger);
    }

    public async Task<Result<NotificationCollection?>> ListNotificationsAsync(
    int limit = 50,
    string? cursor = default,
    DateTime? seenAt = null,
    CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Notification.NotificationListNotifications + $"?limit={limit}";
        if (seenAt is not null)
        {
            url += $"?seenAt={seenAt.Value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)}";
        }

        if (cursor is not null)
        {
            url += $"&cursor={cursor}";
        }

        return await this.Client.Get<NotificationCollection>(url, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}
