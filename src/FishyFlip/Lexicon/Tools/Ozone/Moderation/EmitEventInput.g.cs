// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{
    public partial class EmitEventInput : ATObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmitEventInput"/> class.
        /// </summary>
        /// <param name="@event">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventTakedown"/> (tools.ozone.moderation.defs#modEventTakedown) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventAcknowledge"/> (tools.ozone.moderation.defs#modEventAcknowledge) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventEscalate"/> (tools.ozone.moderation.defs#modEventEscalate) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventComment"/> (tools.ozone.moderation.defs#modEventComment) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventLabel"/> (tools.ozone.moderation.defs#modEventLabel) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventReport"/> (tools.ozone.moderation.defs#modEventReport) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMute"/> (tools.ozone.moderation.defs#modEventMute) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventUnmute"/> (tools.ozone.moderation.defs#modEventUnmute) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMuteReporter"/> (tools.ozone.moderation.defs#modEventMuteReporter) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventUnmuteReporter"/> (tools.ozone.moderation.defs#modEventUnmuteReporter) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventReverseTakedown"/> (tools.ozone.moderation.defs#modEventReverseTakedown) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventResolveAppeal"/> (tools.ozone.moderation.defs#modEventResolveAppeal) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventEmail"/> (tools.ozone.moderation.defs#modEventEmail) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventTag"/> (tools.ozone.moderation.defs#modEventTag) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.AccountEvent"/> (tools.ozone.moderation.defs#accountEvent) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.IdentityEvent"/> (tools.ozone.moderation.defs#identityEvent) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordEvent"/> (tools.ozone.moderation.defs#recordEvent) <br/>
        /// </param>
        /// <param name="subject">
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// </param>
        /// <param name="subjectBlobCids"></param>
        /// <param name="createdBy"></param>
        public EmitEventInput(ATObject @event = default, ATObject subject = default, List<string>? subjectBlobCids = default, FishyFlip.Models.ATDid createdBy = default)
        {
            this.Event = @event;
            this.Subject = subject;
            this.SubjectBlobCids = subjectBlobCids;
            this.CreatedBy = createdBy;
            this.Type = "tools.ozone.moderation.emitEvent#EmitEventInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmitEventInput"/> class.
        /// </summary>
        public EmitEventInput()
        {
            this.Type = "tools.ozone.moderation.emitEvent#EmitEventInput";
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmitEventInput"/> class.
        /// </summary>
        public EmitEventInput(CBORObject obj)
        {
            if (obj["event"] is not null) this.Event = obj["event"].ToATObject();
            if (obj["subject"] is not null) this.Subject = obj["subject"].ToATObject();
            if (obj["subjectBlobCids"] is not null) this.SubjectBlobCids = obj["subjectBlobCids"].Values.Select(n =>n.AsString()).ToList();
            if (obj["createdBy"] is not null) this.CreatedBy = obj["createdBy"].ToATDid();
        }

        /// <summary>
        /// Gets or sets the event.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventTakedown"/> (tools.ozone.moderation.defs#modEventTakedown) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventAcknowledge"/> (tools.ozone.moderation.defs#modEventAcknowledge) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventEscalate"/> (tools.ozone.moderation.defs#modEventEscalate) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventComment"/> (tools.ozone.moderation.defs#modEventComment) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventLabel"/> (tools.ozone.moderation.defs#modEventLabel) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventReport"/> (tools.ozone.moderation.defs#modEventReport) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMute"/> (tools.ozone.moderation.defs#modEventMute) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventUnmute"/> (tools.ozone.moderation.defs#modEventUnmute) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventMuteReporter"/> (tools.ozone.moderation.defs#modEventMuteReporter) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventUnmuteReporter"/> (tools.ozone.moderation.defs#modEventUnmuteReporter) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventReverseTakedown"/> (tools.ozone.moderation.defs#modEventReverseTakedown) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventResolveAppeal"/> (tools.ozone.moderation.defs#modEventResolveAppeal) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventEmail"/> (tools.ozone.moderation.defs#modEventEmail) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventTag"/> (tools.ozone.moderation.defs#modEventTag) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.AccountEvent"/> (tools.ozone.moderation.defs#accountEvent) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.IdentityEvent"/> (tools.ozone.moderation.defs#identityEvent) <br/>
        /// <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordEvent"/> (tools.ozone.moderation.defs#recordEvent) <br/>
        /// </summary>
        [JsonPropertyName("event")]
        [JsonRequired]
        public ATObject Event { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// <br/> Union Types: <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Admin.RepoRef"/> (com.atproto.admin.defs#repoRef) <br/>
        /// <see cref="FishyFlip.Lexicon.Com.Atproto.Repo.StrongRef"/> (com.atproto.repo.strongRef) <br/>
        /// </summary>
        [JsonPropertyName("subject")]
        [JsonRequired]
        public ATObject Subject { get; set; }

        /// <summary>
        /// Gets or sets the subjectBlobCids.
        /// </summary>
        [JsonPropertyName("subjectBlobCids")]
        public List<string>? SubjectBlobCids { get; set; }

        /// <summary>
        /// Gets or sets the createdBy.
        /// </summary>
        [JsonPropertyName("createdBy")]
        [JsonRequired]
        [JsonConverter(typeof(FishyFlip.Tools.Json.ATDidJsonConverter))]
        public FishyFlip.Models.ATDid CreatedBy { get; set; }

        public const string RecordType = "tools.ozone.moderation.emitEvent#EmitEventInput";

        public override string ToJson()
        {
            return JsonSerializer.Serialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.EmitEventInput>(this, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.EmitEventInput>)SourceGenerationContext.Default.ToolsOzoneModerationEmitEventInput)!;
        }

        public static EmitEventInput FromJson(string json)
        {
            return JsonSerializer.Deserialize<FishyFlip.Lexicon.Tools.Ozone.Moderation.EmitEventInput>(json, (JsonTypeInfo<FishyFlip.Lexicon.Tools.Ozone.Moderation.EmitEventInput>)SourceGenerationContext.Default.ToolsOzoneModerationEmitEventInput)!;
        }
    }
}

