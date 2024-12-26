// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Setting
{

    /// <summary>
    /// tools.ozone.setting Endpoint Group.
    /// </summary>
    public static class SettingEndpoints
    {

       public const string ListOptions = "/xrpc/tools.ozone.setting.listOptions";

       public const string RemoveOptions = "/xrpc/tools.ozone.setting.removeOptions";

       public const string UpsertOption = "/xrpc/tools.ozone.setting.upsertOption";


        /// <summary>
        /// List settings with optional filtering
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="limit"></param>
        /// <param name="cursor"></param>
        /// <param name="scope"></param>
        /// <param name="prefix"></param>
        /// <param name="keys"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Setting.ListOptionsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Setting.ListOptionsOutput?>> ListOptionsAsync (this FishyFlip.ATProtocol atp, int? limit = 50, string? cursor = default, string? scope = default, string? prefix = default, List<string>? keys = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = ListOptions.ToString();
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

            if (scope != null)
            {
                queryStrings.Add("scope=" + scope);
            }

            if (prefix != null)
            {
                queryStrings.Add("prefix=" + prefix);
            }

            if (keys != null)
            {
                queryStrings.Add(string.Join("&", keys.Select(n => "keys=" + n)));
            }

            var headers = new Dictionary<string, string>();
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            endpointUrl += string.Join("&", queryStrings);
            return atp.Get<FishyFlip.Lexicon.Tools.Ozone.Setting.ListOptionsOutput>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSettingListOptionsOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Delete settings by key
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="keys"></param>
        /// <param name="scope"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Setting.RemoveOptionsOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Setting.RemoveOptionsOutput?>> RemoveOptionsAsync (this FishyFlip.ATProtocol atp, List<string> keys, string scope, CancellationToken cancellationToken = default)
        {
            var endpointUrl = RemoveOptions.ToString();
            var inputItem = new RemoveOptionsInput();
            inputItem.Keys = keys;
            inputItem.Scope = scope;
            return atp.Post<RemoveOptionsInput, FishyFlip.Lexicon.Tools.Ozone.Setting.RemoveOptionsOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSettingRemoveOptionsInput!, atp.Options.SourceGenerationContext.ToolsOzoneSettingRemoveOptionsOutput!, inputItem, cancellationToken);
        }


        /// <summary>
        /// Create or update setting option
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <param name="managerRole"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Setting.UpsertOptionOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Tools.Ozone.Setting.UpsertOptionOutput?>> UpsertOptionAsync (this FishyFlip.ATProtocol atp, string key, string scope, ATObject value, string? description = default, string? managerRole = default, CancellationToken cancellationToken = default)
        {
            var endpointUrl = UpsertOption.ToString();
            var inputItem = new UpsertOptionInput();
            inputItem.Key = key;
            inputItem.Scope = scope;
            inputItem.Value = value;
            inputItem.Description = description;
            inputItem.ManagerRole = managerRole;
            return atp.Post<UpsertOptionInput, FishyFlip.Lexicon.Tools.Ozone.Setting.UpsertOptionOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ToolsOzoneSettingUpsertOptionInput!, atp.Options.SourceGenerationContext.ToolsOzoneSettingUpsertOptionOutput!, inputItem, cancellationToken);
        }

    }
}

