// <copyright file="OutOfOrderCompletionTracker.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>
using System.Runtime.CompilerServices;

namespace FishyFlip
{
    /// <summary>
    /// Keeps track of the most recent event ID such that all previous events have definitely been processed.
    /// </summary>
    internal class OutOfOrderCompletionTracker
    {
        private readonly Dictionary<long, long?> eventIdToSeq = new();
        private long lastDefinitelyProcessedEventId;
        private long? lastDefinitelyProcessedSeq;
        private long lastGeneratedEventId;

        /// <summary>
        /// Gets the last firehose cursor such that this and all previous events have been already processed on the threadpool.
        /// </summary>
        public long? LastDefinitelyProcessedSeq
        {
            get
            {
                lock (this)
                {
                    return this.lastDefinitelyProcessedSeq;
                }
            }
        }

        /// <summary>
        /// To be called when an event is generated but not processed yet. Each call must eventually result in a <see cref="OnEventProcessed"/>.
        /// </summary>
        /// <returns>A sequentially increasing event ID that must be eventually passed to <see cref="OnEventProcessed"/>.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public long OnEventGenerated()
        {
            return ++this.lastGeneratedEventId;
        }

        /// <summary>
        /// To be called when an event is fully processed (successfully or unsuccessfully). Each call must come after a corresponding <see cref="OnEventGenerated"/>.
        /// </summary>
        /// <param name="eventId">The event ID.</param>
        /// <param name="seq">The firehose cursor of the current entry, if available.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void OnEventProcessed(long eventId, long? seq)
        {
            if (eventId <= this.lastDefinitelyProcessedEventId)
            {
                throw new InvalidOperationException("OnEventProcessed was called with an eventId that was already supposedly processed.");
            }

            this.eventIdToSeq.Add(eventId, seq);
            this.UpdateLastDefinitelyProcessedEvent();
        }

        private void UpdateLastDefinitelyProcessedEvent()
        {
            while (this.eventIdToSeq.TryGetValue(this.lastDefinitelyProcessedEventId + 1, out var seq))
            {
                this.lastDefinitelyProcessedEventId++;
                if (seq != null)
                {
                    this.lastDefinitelyProcessedSeq = seq;
                }

                this.eventIdToSeq.Remove(this.lastDefinitelyProcessedEventId);
            }
        }
    }
}


