// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Set
{

    /// <summary>
    /// tools.ozone.set Endpoint Group.
    /// </summary>
    public static class SetEndpoints
    {

       public const string AddValues = "/xrpc/tools.ozone.set.addValues";

       public const string DeleteSet = "/xrpc/tools.ozone.set.deleteSet";

       public const string DeleteValues = "/xrpc/tools.ozone.set.deleteValues";

       public const string GetValues = "/xrpc/tools.ozone.set.getValues";

       public const string QuerySets = "/xrpc/tools.ozone.set.querySets";

       public const string UpsertSet = "/xrpc/tools.ozone.set.upsertSet";


        /// <summary>
        /// Add values to a specific set. Attempting to add values to a set that does not exist will result in an error.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> AddValuesAsync (this FishyFlip.ATProtocol atp, string name, List<string> values, CancellationToken cancellationToken = default)
        {
            var endpointUrl = AddValues.ToString();
            var inputItem = new AddValuesInput();
            inputItem.Name = name;
            inputItem.Values = values;
            return atp.Client.Post<AddValuesInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSetAddValuesInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Delete an entire set. Attempting to delete a set that does not exist will result in an error.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Set.DeleteSetOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.DeleteSetOutput?>> DeleteSetAsync (this FishyFlip.ATProtocol atp, string name, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DeleteSet.ToString();
            var inputItem = new DeleteSetInput();
            inputItem.Name = name;
            return atp.Client.Post<DeleteSetInput, FishyFlip.Lexicon.Tools.Ozone.Set.DeleteSetOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSetDeleteSetInput!, atp.Options.SourceGenerationContext.ToolsOzoneSetDeleteSetOutput!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Delete values from a specific set. Attempting to delete values that are not in the set will not result in an error
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> DeleteValuesAsync (this FishyFlip.ATProtocol atp, string name, List<string> values, CancellationToken cancellationToken = default)
        {
            var endpointUrl = DeleteValues.ToString();
            var inputItem = new DeleteValuesInput();
            inputItem.Name = name;
            inputItem.Values = values;
            return atp.Client.Post<DeleteValuesInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSetDeleteValuesInput!, atp.Options.SourceGenerationContext.Success!, atp.Options.JsonSerializerOptions, inputItem, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Get a specific set and its values
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="name"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput?>> GetValuesAsync (this FishyFlip.ATProtocol atp, string name, int? limit = 100, string? cursor = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = GetValues.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            queryStrings.Add("name=" + name);

            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSetGetValuesOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Query available sets
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="namePrefix"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDirection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Set.QuerySetsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.QuerySetsOutput?>> QuerySetsAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? namePrefix = default, string? sortBy = default, string? sortDirection = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = QuerySets.ToString();
            endpointUrl += "?";
            List<string> queryStrings = new();
            if (limit != null)
            {
                queryStrings.Add("limit=" + limit);
            }

            if (cursor != null)
            {
                queryStrings.Add("cursor=" + cursor);
            }

            if (namePrefix != null)
            {
                queryStrings.Add("namePrefix=" + namePrefix);
            }

            if (sortBy != null)
            {
                queryStrings.Add("sortBy=" + sortBy);
            }

            if (sortDirection != null)
            {
                queryStrings.Add("sortDirection=" + sortDirection);
            }

            endpointUrl += string.Join("&", queryStrings);
            return atp.Client.Get<FishyFlip.Lexicon.Tools.Ozone.Set.QuerySetsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSetQuerySetsOutput!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }


        /// <summary>
        /// Create or update set metadata
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Set.SetView?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.SetView?>> UpsertSetAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UpsertSet.ToString();
            return atp.Client.Post<FishyFlip.Lexicon.Tools.Ozone.Set.SetView?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSetSetView!, atp.Options.JsonSerializerOptions, cancellationToken, atp.Options.Logger);
        }

    }
}

