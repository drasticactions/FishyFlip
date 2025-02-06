// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Community.Lexicon.Calendar
{
    /// <summary>
    /// An RSVP for an event.
    /// </summary>
    public partial class Rsvp : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Rsvp"/> class.
        /// </summary>
        /// <param name="subject">
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </param>
        /// <param name="status">
        /// <br/> Known Values: <br/>
        /// interested - Interested in the event <br/>
        /// going - Going to the event <br/>
        /// notgoing - Not going to the event <br/>
        /// </param>
        public Rsvp(Com.Atproto.Repo.StrongRef? subject, string? status)
        {
            this.Subject = subject;
            this.Status = status;
            this.Type = "community.lexicon.calendar.rsvp";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rsvp"/> class.
        /// </summary>
        public Rsvp()
        {
            this.Type = "community.lexicon.calendar.rsvp";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rsvp"/> class.
        /// </summary>
        public Rsvp(CBORObject obj)
        {
            if (obj["subject"] is not null) this.Subject = new FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef(obj["subject"]);
            if (obj["status"] is not null) this.Status = obj["status"].AsString();
        }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef)
        /// </summary>
        [JsonPropertyName("subject")]
        public Com.Atproto.Repo.StrongRef? Subject { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// <br/> Known Values: <br/>
        /// interested - Interested in the event <br/>
        /// going - Going to the event <br/>
        /// notgoing - Not going to the event <br/>
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; } = "community.lexicon.calendar.rsvp#going";

        public const string RecordType = "community.lexicon.calendar.rsvp";

        public static Rsvp FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Community.Lexicon.Calendar.Rsvp>(json, (JsonTypeInfo<FishyFlip.Lexicon.Community.Lexicon.Calendar.Rsvp>)SourceGenerationContext.Default.CommunityLexiconCalendarRsvp)!;
        }
    }
}

