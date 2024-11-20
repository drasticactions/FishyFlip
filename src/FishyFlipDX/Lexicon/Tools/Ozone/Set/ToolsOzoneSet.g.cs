// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable

namespace FishyFlip.Lexicon.Tools.Ozone.Set
{

    /// <summary>
    /// tools.ozone.set Endpoint Class.
    /// </summary>
    public sealed class ToolsOzoneSet
    {

        private ATProtocol atp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsOzoneSet"/> class.
        /// </summary>
        /// <param name="atp"><see cref="ATProtocol"/>.</param>
        internal ToolsOzoneSet(ATProtocol atp)
        {
            this.atp = atp;
        }

        /// <summary>
        /// Gets the ATProtocol.
        /// </summary>
        internal ATProtocol ATProtocol => this.atp;


        /// <summary>
        /// Add values to a specific set. Attempting to add values to a set that does not exist will result in an error.
        /// </summary>
        public Task<Result<Success?>> AddValuesAsync (string name, List<string?> values, CancellationToken cancellationToken = default)
        {
            return atp.AddValuesAsync(name, values, cancellationToken);
        }


        /// <summary>
        /// Delete an entire set. Attempting to delete a set that does not exist will result in an error.
        /// </summary>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.DeleteSetOutput?>> DeleteSetAsync (string name, CancellationToken cancellationToken = default)
        {
            return atp.DeleteSetAsync(name, cancellationToken);
        }


        /// <summary>
        /// Delete values from a specific set. Attempting to delete values that are not in the set will not result in an error
        /// </summary>
        public Task<Result<Success?>> DeleteValuesAsync (string name, List<string?> values, CancellationToken cancellationToken = default)
        {
            return atp.DeleteValuesAsync(name, values, cancellationToken);
        }


        /// <summary>
        /// Get a specific set and its values
        /// </summary>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.GetValuesOutput?>> GetValuesAsync (string name, int? limit = 100, string? cursor = default, CancellationToken cancellationToken = default)
        {
            return atp.GetValuesAsync(name, limit, cursor, cancellationToken);
        }


        /// <summary>
        /// Query available sets
        /// </summary>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.QuerySetsOutput?>> QuerySetsAsync (int? limit = 50, string? cursor = default, string? namePrefix = default, string? sortBy = default, string? sortDirection = default, CancellationToken cancellationToken = default)
        {
            return atp.QuerySetsAsync(limit, cursor, namePrefix, sortBy, sortDirection, cancellationToken);
        }


        /// <summary>
        /// Create or update set metadata
        /// </summary>
        public Task<Result<FishyFlip.Lexicon.Tools.Ozone.Set.SetView?>> UpsertSetAsync (CancellationToken cancellationToken = default)
        {
            return atp.UpsertSetAsync(cancellationToken);
        }

    }
}
