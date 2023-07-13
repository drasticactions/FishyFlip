// <copyright file="Follow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models
{
    public class Follow : ATRecord
    {
        [JsonConstructor]
        public Follow(ATDid? subject, DateTime? createdAt, string? type)
            : base(type)
        {
            this.Subject = subject;
            this.CreatedAt = createdAt;
        }

        public Follow(CBORObject obj)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
            this.Type = Constants.GraphTypes.Follow;
            this.Subject = ATDid.Create(obj["subject"].AsString());
        }

        public ATDid? Subject { get; }

        public DateTime? CreatedAt { get; }
    }
}
