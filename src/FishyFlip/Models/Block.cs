// <copyright file="Block.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models
{
    public class Block : ATRecord
    {
        public Block(CBORObject obj)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
            this.Type = Constants.GraphTypes.Follow;
            this.Did = AtDid.Create(obj["subject"].AsString());
        }

        public AtDid Did { get; }

        public DateTime? CreatedAt { get; }
    }
}