// <copyright file="Block.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models
{
    /// <summary>
    /// Firehose Block.
    /// </summary>
    public class Block : ATRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        /// <param name="obj">CBorObject.</param>
        public Block(CBORObject obj)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
            this.Type = Constants.GraphTypes.Block;
            this.Did = ATDid.Create(obj["subject"].AsString());
        }

        /// <summary>
        /// Gets the ATDid.
        /// </summary>
        public ATDid? Did { get; }

        /// <summary>
        /// Gets the Created At Date.
        /// </summary>
        public DateTime? CreatedAt { get; }
    }
}