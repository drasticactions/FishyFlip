// <copyright file="BlueskyChat.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip;

/// <summary>
/// Bluesky Chat.
/// </summary>
public sealed class BlueskyChat
{
    private readonly ATProtocol proto;
    private readonly Dictionary<string, string> chatHeaders;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlueskyChat"/> class.
    /// </summary>
    /// <param name="proto"><see cref="ATProtocol"/>.</param>
    internal BlueskyChat(ATProtocol proto)
    {
        this.proto = proto;
        this.chatHeaders = new Dictionary<string, string>
        {
            { Constants.ATProtoProxy.Proxy, Constants.ATProtoProxy.BskyChat },
        };
    }

    private ATProtocolOptions Options => this.proto.Options;

    private HttpClient Client => this.proto.Client;

    /// <summary>
    /// Retrieves the messages of a conversation asynchronously.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="cursor">The cursor for pagination in the conversation.</param>
    /// <param name="limit">Limit of conversations to get, defaults to 60.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{ConversationMessages?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationMessages?>> GetConversationMessagesAsync(string convoId, string? cursor = default, int limit = 60, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.Bluesky.Chat.Convo.GetMessages}?convoId={convoId}";

        if (!string.IsNullOrWhiteSpace(cursor))
        {
            url += $"&cursor={cursor}";
        }

        if (limit > 0)
        {
            url += $"&limit={limit}";
        }

        return this.Client.Get<ConversationMessages>(url, this.Options.SourceGenerationContext.ConversationMessages, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Retrieves a list of conversations asynchronously.
    /// </summary>
    /// <param name="cursor">An optional string that represents the cursor position in the list. Default is an empty string.</param>
    /// <param name="limit">Limit of conversations to get, defaults to 60.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{ConversationList?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationList?>> GetConversationsAsync(string cursor = "", int limit = 60, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.Bluesky.Chat.Convo.ListConvos}?limit={limit}";

        if (!string.IsNullOrWhiteSpace(cursor))
        {
            url += $"&cursor={cursor}";
        }

        return this.Client.Get<ConversationList>(url, this.Options.SourceGenerationContext.ConversationList, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Retrieves a conversation asynchronously.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Conversation?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationView?>> GetConversationAsync(string convoId, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.Bluesky.Chat.Convo.GetConvo}?convoId={convoId}";
        return this.Client.Get<ConversationView>(url, this.Options.SourceGenerationContext.ConversationView, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Retrieves a conversation for members asynchronously.
    /// </summary>
    /// <param name="members">The ATDid of the member of the conversation.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Conversation?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationView?>> GetConversationForMembersAsync(ATDid[] members, CancellationToken cancellationToken = default)
    {
        var commaSeparatedMembers = string.Join(",", members.Select(x => x.ToString()));
        var url = $"{Constants.Urls.Bluesky.Chat.Convo.GetConvoForMembers}?members={commaSeparatedMembers}";
        return this.Client.Get<ConversationView>(url, this.Options.SourceGenerationContext.ConversationView, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Sends a message asynchronously in a conversation.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{MessageView}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<MessageView>> SendMessageAsync(string convoId, string message, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.Bluesky.Chat.Convo.SendMessage}";
        var createMessage = new CreateMessage(convoId, new CreateMessageMessage(message));

        return this.Client.Post<CreateMessage, MessageView>(url, this.Options.SourceGenerationContext.CreateMessage, this.Options.SourceGenerationContext.MessageView, this.Options.JsonSerializerOptions, createMessage, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Update the read status of a conversation asynchronously.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Conversation?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationView>> UpdateReadAsync(string convoId, CancellationToken cancellationToken = default)
    {
        var updateRead = new UpdateRead(convoId);
        return this.Client.Post<UpdateRead, ConversationView>(Constants.Urls.Bluesky.Chat.Convo.UpdateRead, this.Options.SourceGenerationContext.UpdateRead, this.Options.SourceGenerationContext.ConversationView, this.Options.JsonSerializerOptions, updateRead, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Mute a conversation asynchronously.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Conversation?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationView>> MuteConvoAsync(string convoId, CancellationToken cancellationToken = default)
    {
        var updateRead = new UpdateRead(convoId);
        return this.Client.Post<UpdateRead, ConversationView>(Constants.Urls.Bluesky.Chat.Convo.MuteConvo, this.Options.SourceGenerationContext.UpdateRead, this.Options.SourceGenerationContext.ConversationView, this.Options.JsonSerializerOptions, updateRead, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Unmute a conversation asynchronously.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Conversation?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<ConversationView>> UnmuteConvoAsync(string convoId, CancellationToken cancellationToken = default)
    {
        var updateRead = new UpdateRead(convoId);
        return this.Client.Post<UpdateRead, ConversationView>(Constants.Urls.Bluesky.Chat.Convo.UnmuteConvo, this.Options.SourceGenerationContext.UpdateRead, this.Options.SourceGenerationContext.ConversationView, this.Options.JsonSerializerOptions, updateRead, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Retrieves the log asynchronously.
    /// </summary>
    /// <param name="cursor">The cursor for pagination in the log.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{LogResponse?}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<LogResponse?>> GetLogAsync(string cursor, CancellationToken cancellationToken = default)
    {
        var url = $"{Constants.Urls.Bluesky.Chat.Convo.GetLog}?cursor={cursor}";
        return this.Client.Get<LogResponse>(url, this.Options.SourceGenerationContext.LogResponse, this.Options.JsonSerializerOptions, cancellationToken, this.Options.Logger, this.chatHeaders);
    }

    /// <summary>
    /// Leaves a conversation asynchronously.
    /// </summary>
    /// <param name="convoId">The unique identifier of the conversation.</param>
    /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{LeaveConvoResponse}"/> that encapsulates the result of the operation.</returns>
    public Task<Result<LeaveConvoResponse>> LeaveConvoAsync(string convoId, CancellationToken cancellationToken = default)
    {
        var updateRead = new UpdateRead(convoId);
        return this.Client.Post<UpdateRead, LeaveConvoResponse>(Constants.Urls.Bluesky.Chat.Convo.LeaveConvo, this.Options.SourceGenerationContext.UpdateRead, this.Options.SourceGenerationContext.LeaveConvoResponse, this.Options.JsonSerializerOptions, updateRead, cancellationToken, this.Options.Logger, this.chatHeaders);
    }
}