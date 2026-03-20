// <copyright file="JetStreamATWebSocketRecordErrorEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Events
{
    /// <summary>
    /// Provides event data for errors encountered while parsing a JetStream AT WebSocket record.
    /// </summary>
    public class JetStreamATWebSocketRecordErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JetStreamATWebSocketRecordErrorEventArgs"/> class.
        /// </summary>
        /// <param name="eventId">The event ID of the record whose parsing failed.</param>
        public JetStreamATWebSocketRecordErrorEventArgs(long eventId)
        {
            this.EventId = eventId;
        }

        /// <summary>
        /// Gets the event ID of the record whose parsing failed.
        /// </summary>
        public long EventId { get; }
    }
}
