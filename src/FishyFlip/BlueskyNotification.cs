// <copyright file="BlueskyNotification.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;

namespace FishyFlip;

/// <summary>
/// Bluesky Notification.
/// </summary>
public sealed class BlueskyNotification
{
    private ATProtocol proto;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyNotification"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal BlueskyNotification(ATProtocol proto)
    {
        this.proto = proto;
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Asynchronously retrieves the count of unread notifications.
    /// </summary>
    /// <param name="seenAt">Optional. The date and time at which the notifications were last seen. If provided, only notifications received after this date and time will be counted.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the count of unread notifications, or null if the count could not be retrieved.</returns>
    public Task<Result<UnreadCount?>> GetUnreadCountAsync(DateTime? seenAt = null, CancellationToken cancellationToken = default)
    {
        var url = Constants.Urls.Bluesky.Notification.NotificationGetUnreadCount;
        if (seenAt is not null)
        {
            url += $"?seenAt={seenAt.Value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)}";
        }

        return this.Client.Get<UnreadCount>(url, this.Options.SourceGenerationContext.UnreadCount, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously updates the date and time at which the notifications were last seen.
    /// </summary>
    /// <param name="seenAt">The date and time at which the notifications were last seen.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object indicating whether the operation was successful, or null if the operation failed.</returns>
    public Task<Result<Success?>> UpdateSeenAsync(DateTime seenAt, CancellationToken cancellationToken = default)
    {
        var createSeenAtRecord = new CreateSeenAtRecord(seenAt.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture));
        var url = Constants.Urls.Bluesky.Notification.NotificationUpdateSeen;

        return this.Client.Post<CreateSeenAtRecord, Success?>(url, this.Options.SourceGenerationContext.CreateSeenAtRecord, this.Options.SourceGenerationContext.Success!, this.Options.JsonSerializerOptions, createSeenAtRecord, cancellationToken, this.Options.Logger);
    }

    /// <summary>
    /// Asynchronously retrieves a list of notifications.
    /// </summary>
    /// <param name="limit">Optional. The maximum number of notifications to retrieve. Default is 50.</param>
    /// <param name="cursor">Optional. A cursor that can be used to paginate through notifications.</param>
    /// <param name="seenAt">Optional. The date and time at which the notifications were last seen. If provided, only notifications received after this date and time will be retrieved.</param>
    /// <param name="cancellationToken">Optional. A CancellationToken that can be used to cancel the operation.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a Result object with the list of notifications, or null if no notifications were found.</returns>
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

        return await this.Client.Get<NotificationCollection>(url, this.Options.SourceGenerationContext.NotificationCollection, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger);
    }
}
