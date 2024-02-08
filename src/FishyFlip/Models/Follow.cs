// <copyright file="Follow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models
{
    /// <summary>
    /// Represents a follow action in the system.
    /// </summary>
    public class Follow : ATRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Follow"/> class.
        /// </summary>
        /// <param name="subject">The subject of the follow action.</param>
        /// <param name="createdAt">The date and time when the follow action was created.</param>
        /// <param name="type">The type of the follow action.</param>
        [JsonConstructor]
        public Follow(ATDid? subject, DateTime? createdAt, string? type)
            : base(type)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Follow"/> class from a CBOR object.
        /// </summary>
        /// <param name="obj">The CBOR object representing the follow action.</param>
        public Follow(CBORObject obj)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
            this.Type = Constants.GraphTypes.Follow;
            this.Subject = ATDid.Create(obj["subject"].AsString());
        }

        /// <summary>
        /// Gets the subject of the follow action.
        /// </summary>
        public ATDid? Subject { get; }

        /// <summary>
        /// Gets the date and time when the follow action was created.
        /// </summary>
        public DateTime? CreatedAt { get; }
    }
}
