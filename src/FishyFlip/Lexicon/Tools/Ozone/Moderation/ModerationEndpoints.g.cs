// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Tools.Ozone.Moderation
{

    /// <summary>
    /// tools.ozone.moderation Endpoint Group.
    /// </summary>
    public static class ModerationEndpoints
    {

       public const string EmitEvent = "/xrpc/tools.ozone.moderation.emitEvent";

       public const string GetEvent = "/xrpc/tools.ozone.moderation.getEvent";

       public const string GetRecord = "/xrpc/tools.ozone.moderation.getRecord";

       public const string GetRecords = "/xrpc/tools.ozone.moderation.getRecords";

       public const string GetRepo = "/xrpc/tools.ozone.moderation.getRepo";

       public const string GetReporterStats = "/xrpc/tools.ozone.moderation.getReporterStats";

       public const string GetRepos = "/xrpc/tools.ozone.moderation.getRepos";

       public const string QueryEvents = "/xrpc/tools.ozone.moderation.queryEvents";

       public const string QueryStatuses = "/xrpc/tools.ozone.moderation.queryStatuses";

       public const string SearchRepos = "/xrpc/tools.ozone.moderation.searchRepos";


        /// <summary>
        /// Take a moderation action on an actor.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.SubjectHasActionError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="@event"></param>
        /// <param name="subject"></param>
        /// <param name="createdBy"></param>
        /// <param name="subjectBlobCids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventView?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventView?>> EmitEventAsync (this FishyFlip.ATProtocol atp, ATObject @event, ATObject subject, FishyFlip.Models.ATDid createdBy, List<string>? subjectBlobCids = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = EmitEvent.ToString();
            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            var inputItem = new EmitEventInput();
            switch (@event.Type)
            {
                case "tools.ozone.moderation.defs#modEventTakedown":
                case "tools.ozone.moderation.defs#modEventAcknowledge":
                case "tools.ozone.moderation.defs#modEventEscalate":
                case "tools.ozone.moderation.defs#modEventComment":
                case "tools.ozone.moderation.defs#modEventLabel":
                case "tools.ozone.moderation.defs#modEventReport":
                case "tools.ozone.moderation.defs#modEventMute":
                case "tools.ozone.moderation.defs#modEventUnmute":
                case "tools.ozone.moderation.defs#modEventMuteReporter":
                case "tools.ozone.moderation.defs#modEventUnmuteReporter":
                case "tools.ozone.moderation.defs#modEventReverseTakedown":
                case "tools.ozone.moderation.defs#modEventResolveAppeal":
                case "tools.ozone.moderation.defs#modEventEmail":
                case "tools.ozone.moderation.defs#modEventTag":
                case "tools.ozone.moderation.defs#accountEvent":
                case "tools.ozone.moderation.defs#identityEvent":
                case "tools.ozone.moderation.defs#recordEvent":
                case "tools.ozone.moderation.defs#modEventPriorityScore":
                    break;
                default:
                    atp.Options.Logger?.LogWarning($"Unknown @event type for union: " + @event.Type);
                    break;
            }
            inputItem.Event = @event;
            switch (subject.Type)
            {
                case "com.atproto.admin.defs#repoRef":
                case "com.atproto.repo.strongRef":
                    break;
                default:
                    atp.Options.Logger?.LogWarning($"Unknown subject type for union: " + subject.Type);
                    break;
            }
            inputItem.Subject = subject;
            inputItem.CreatedBy = createdBy;
            inputItem.SubjectBlobCids = subjectBlobCids;
            return atp.Post<EmitEventInput, FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventView?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationEmitEventInput!, atp.Options.SourceGenerationContext.ToolsOzoneModerationModEventView!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Get details about a moderation event.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventViewDetail?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventViewDetail?>> GetEventAsync (this FishyFlip.ATProtocol atp, int id, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetEvent.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("id=" + id);

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.ModEventViewDetail>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationModEventViewDetail!, cancellationToken, headers);
        }


        /// <summary>
        /// Get details about a record.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RecordNotFoundError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uri"></param>
        /// <param name="cid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewDetail?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewDetail?>> GetRecordAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATUri uri, string? cid = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRecord.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("uri=" + uri);

            if (cid != null)
            {
                queryStrings.Add("cid=" + cid);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.RecordViewDetail>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationRecordViewDetail!, cancellationToken, headers);
        }


        /// <summary>
        /// Get details about some records.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="uris"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.GetRecordsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetRecordsOutput?>> GetRecordsAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATUri> uris, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRecords.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", uris.Select(n => "uris=" + n)));

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetRecordsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationGetRecordsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get details about a repository.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.RepoNotFoundError"/>  <br/>
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewDetail?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewDetail?>> GetRepoAsync (this FishyFlip.ATProtocol atp, FishyFlip.Models.ATDid did, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRepo.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("did=" + did);

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.RepoViewDetail>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationRepoViewDetail!, cancellationToken, headers);
        }


        /// <summary>
        /// Get reporter stats for a list of users.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="dids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput?>> GetReporterStatsAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATDid> dids, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetReporterStats.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", dids.Select(n => "dids=" + n)));

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReporterStatsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationGetReporterStatsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Get details about some repositories.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="dids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput?>> GetReposAsync (this FishyFlip.ATProtocol atp, List<FishyFlip.Models.ATDid> dids, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetRepos.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add(string.Join("&", dids.Select(n => "dids=" + n)));

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.GetReposOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationGetReposOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// List moderation events related to a subject.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="types"></param>
        /// <param name="createdBy"></param>
        /// <param name="sortDirection"></param>
        /// <param name="createdAfter"></param>
        /// <param name="createdBefore"></param>
        /// <param name="subject"></param>
        /// <param name="collections"></param>
        /// <param name="subjectType"></param>
        /// <param name="includeAllUserRecords"></param>
        /// <param name="limit"></param>
        /// <param name="hasComment"></param>
        /// <param name="comment"></param>
        /// <param name="addedLabels"></param>
        /// <param name="removedLabels"></param>
        /// <param name="addedTags"></param>
        /// <param name="removedTags"></param>
        /// <param name="reportTypes"></param>
        /// <param name="policies"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryEventsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryEventsOutput?>> QueryEventsAsync (this FishyFlip.ATProtocol atp, List<string>? types = default, FishyFlip.Models.ATDid? createdBy = default, string? sortDirection = default, DateTime? createdAfter = default, DateTime? createdBefore = default, string? subject = default, List<string>? collections = default, string? subjectType = default, bool? includeAllUserRecords = default, int? limit = 50, bool? hasComment = default, string? comment = default, List<string>? addedLabels = default, List<string>? removedLabels = default, List<string>? addedTags = default, List<string>? removedTags = default, List<string>? reportTypes = default, List<string>? policies = default, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = QueryEvents.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (types != null)
            {
                queryStrings.Add(string.Join("&", types.Select(n => "types=" + n)));
            }

            if (createdBy != null)
            {
                queryStrings.Add("createdBy=" + createdBy);
            }

            if (sortDirection != null)
            {
                queryStrings.Add("sortDirection=" + sortDirection);
            }

            if (createdAfter != null)
            {
                queryStrings.Add("createdAfter=" + createdAfter);
            }

            if (createdBefore != null)
            {
                queryStrings.Add("createdBefore=" + createdBefore);
            }

            if (subject != null)
            {
                queryStrings.Add("subject=" + subject);
            }

            if (collections != null)
            {
                queryStrings.Add(string.Join("&", collections.Select(n => "collections=" + n)));
            }

            if (subjectType != null)
            {
                queryStrings.Add("subjectType=" + subjectType);
            }

            if (includeAllUserRecords != null)
            {
                queryStrings.Add("includeAllUserRecords=" + (includeAllUserRecords.Value ? "true" : "false"));
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (hasComment != null)
            {
                queryStrings.Add("hasComment=" + (hasComment.Value ? "true" : "false"));
            }

            if (comment != null)
            {
                queryStrings.Add("comment=" + comment);
            }

            if (addedLabels != null)
            {
                queryStrings.Add(string.Join("&", addedLabels.Select(n => "addedLabels=" + n)));
            }

            if (removedLabels != null)
            {
                queryStrings.Add(string.Join("&", removedLabels.Select(n => "removedLabels=" + n)));
            }

            if (addedTags != null)
            {
                queryStrings.Add(string.Join("&", addedTags.Select(n => "addedTags=" + n)));
            }

            if (removedTags != null)
            {
                queryStrings.Add(string.Join("&", removedTags.Select(n => "removedTags=" + n)));
            }

            if (reportTypes != null)
            {
                queryStrings.Add(string.Join("&", reportTypes.Select(n => "reportTypes=" + n)));
            }

            if (policies != null)
            {
                queryStrings.Add(string.Join("&", policies.Select(n => "policies=" + n)));
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryEventsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationQueryEventsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// View moderation statuses of subjects (record or repo).
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="queueCount"></param>
        /// <param name="queueIndex"></param>
        /// <param name="queueSeed"></param>
        /// <param name="includeAllUserRecords"></param>
        /// <param name="subject"></param>
        /// <param name="comment"></param>
        /// <param name="reportedAfter"></param>
        /// <param name="reportedBefore"></param>
        /// <param name="reviewedAfter"></param>
        /// <param name="hostingDeletedAfter"></param>
        /// <param name="hostingDeletedBefore"></param>
        /// <param name="hostingUpdatedAfter"></param>
        /// <param name="hostingUpdatedBefore"></param>
        /// <param name="hostingStatuses"></param>
        /// <param name="reviewedBefore"></param>
        /// <param name="includeMuted"></param>
        /// <param name="onlyMuted"></param>
        /// <param name="reviewState"></param>
        /// <param name="ignoreSubjects"></param>
        /// <param name="lastReviewedBy"></param>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="takendown"></param>
        /// <param name="appealed"></param>
        /// <param name="limit"></param>
        /// <param name="tags"></param>
        /// <param name="excludeTags"></param>
        /// <param name="cursor"></param>
        /// <param name="collections"></param>
        /// <param name="subjectType"></param>
        /// <param name="minAccountSuspendCount"></param>
        /// <param name="minReportedRecordsCount"></param>
        /// <param name="minTakendownRecordsCount"></param>
        /// <param name="minPriorityScore"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput?>> QueryStatusesAsync (this FishyFlip.ATProtocol atp, int? queueCount = 0, int? queueIndex = 0, string? queueSeed = default, bool? includeAllUserRecords = default, string? subject = default, string? comment = default, DateTime? reportedAfter = default, DateTime? reportedBefore = default, DateTime? reviewedAfter = default, DateTime? hostingDeletedAfter = default, DateTime? hostingDeletedBefore = default, DateTime? hostingUpdatedAfter = default, DateTime? hostingUpdatedBefore = default, List<string>? hostingStatuses = default, DateTime? reviewedBefore = default, bool? includeMuted = default, bool? onlyMuted = default, string? reviewState = default, List<string>? ignoreSubjects = default, FishyFlip.Models.ATDid? lastReviewedBy = default, string? sortField = default, string? sortDirection = default, bool? takendown = default, bool? appealed = default, int? limit = 50, List<string>? tags = default, List<string>? excludeTags = default, string? cursor = default, List<string>? collections = default, string? subjectType = default, int? minAccountSuspendCount = 0, int? minReportedRecordsCount = 0, int? minTakendownRecordsCount = 0, int? minPriorityScore = 0, CancellationToken cancellationToken = default)
        {
            var endpointUrl = QueryStatuses.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (queueCount != null)
            {
                queryStrings.Add("queueCount=" + queueCount);
            }

            if (queueIndex != null)
            {
                queryStrings.Add("queueIndex=" + queueIndex);
            }

            if (queueSeed != null)
            {
                queryStrings.Add("queueSeed=" + queueSeed);
            }

            if (includeAllUserRecords != null)
            {
                queryStrings.Add("includeAllUserRecords=" + (includeAllUserRecords.Value ? "true" : "false"));
            }

            if (subject != null)
            {
                queryStrings.Add("subject=" + subject);
            }

            if (comment != null)
            {
                queryStrings.Add("comment=" + comment);
            }

            if (reportedAfter != null)
            {
                queryStrings.Add("reportedAfter=" + reportedAfter);
            }

            if (reportedBefore != null)
            {
                queryStrings.Add("reportedBefore=" + reportedBefore);
            }

            if (reviewedAfter != null)
            {
                queryStrings.Add("reviewedAfter=" + reviewedAfter);
            }

            if (hostingDeletedAfter != null)
            {
                queryStrings.Add("hostingDeletedAfter=" + hostingDeletedAfter);
            }

            if (hostingDeletedBefore != null)
            {
                queryStrings.Add("hostingDeletedBefore=" + hostingDeletedBefore);
            }

            if (hostingUpdatedAfter != null)
            {
                queryStrings.Add("hostingUpdatedAfter=" + hostingUpdatedAfter);
            }

            if (hostingUpdatedBefore != null)
            {
                queryStrings.Add("hostingUpdatedBefore=" + hostingUpdatedBefore);
            }

            if (hostingStatuses != null)
            {
                queryStrings.Add(string.Join("&", hostingStatuses.Select(n => "hostingStatuses=" + n)));
            }

            if (reviewedBefore != null)
            {
                queryStrings.Add("reviewedBefore=" + reviewedBefore);
            }

            if (includeMuted != null)
            {
                queryStrings.Add("includeMuted=" + (includeMuted.Value ? "true" : "false"));
            }

            if (onlyMuted != null)
            {
                queryStrings.Add("onlyMuted=" + (onlyMuted.Value ? "true" : "false"));
            }

            if (reviewState != null)
            {
                queryStrings.Add("reviewState=" + reviewState);
            }

            if (ignoreSubjects != null)
            {
                queryStrings.Add(string.Join("&", ignoreSubjects.Select(n => "ignoreSubjects=" + n)));
            }

            if (lastReviewedBy != null)
            {
                queryStrings.Add("lastReviewedBy=" + lastReviewedBy);
            }

            if (sortField != null)
            {
                queryStrings.Add("sortField=" + sortField);
            }

            if (sortDirection != null)
            {
                queryStrings.Add("sortDirection=" + sortDirection);
            }

            if (takendown != null)
            {
                queryStrings.Add("takendown=" + (takendown.Value ? "true" : "false"));
            }

            if (appealed != null)
            {
                queryStrings.Add("appealed=" + (appealed.Value ? "true" : "false"));
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (tags != null)
            {
                queryStrings.Add(string.Join("&", tags.Select(n => "tags=" + n)));
            }

            if (excludeTags != null)
            {
                queryStrings.Add(string.Join("&", excludeTags.Select(n => "excludeTags=" + n)));
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            if (collections != null)
            {
                queryStrings.Add(string.Join("&", collections.Select(n => "collections=" + n)));
            }

            if (subjectType != null)
            {
                queryStrings.Add("subjectType=" + subjectType);
            }

            if (minAccountSuspendCount != null)
            {
                queryStrings.Add("minAccountSuspendCount=" + minAccountSuspendCount);
            }

            if (minReportedRecordsCount != null)
            {
                queryStrings.Add("minReportedRecordsCount=" + minReportedRecordsCount);
            }

            if (minTakendownRecordsCount != null)
            {
                queryStrings.Add("minTakendownRecordsCount=" + minTakendownRecordsCount);
            }

            if (minPriorityScore != null)
            {
                queryStrings.Add("minPriorityScore=" + minPriorityScore);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.QueryStatusesOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationQueryStatusesOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Find repositories based on a search term.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="q"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput?>> SearchReposAsync (this FishyFlip.ATProtocol atp, string? q = default, int? limit = 50, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = SearchRepos.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (q != null)
            {
                queryStrings.Add("q=" + q);
            }

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoProxy, atp.Options.OzoneProxyHeader);
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Moderation.SearchReposOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneModerationSearchReposOutput!, cancellationToken, headers);
        }

    }
}

