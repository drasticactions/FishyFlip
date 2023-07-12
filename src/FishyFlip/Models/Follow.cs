// <copyright file="Follow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models
{
    public class Follow : ATRecord
    {
        public Follow(CBORObject obj)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
            this.Type = Constants.GraphTypes.Follow;
            this.Did = ATDid.Create(obj["subject"].AsString());
        }

        public ATDid Did { get; }

        public DateTime? CreatedAt { get; }
    }
}
