// <copyright file="DnsClientResolver.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools;

/// <summary>
/// Uses DNS to resolve AT identifiers.
/// </summary>
public class DnsClientResolver : IATIdentifierResolver
{
    private ILogger? logger;
    private LookupClient dnsClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="DnsClientResolver"/> class.
    /// </summary>
    /// <param name="client">The DNS client.</param>
    /// <param name="logger">The logger.</param>
    /// </summary>
    public DnsClientResolver(LookupClient client, ILogger? logger = null)
    {
        this.logger = logger;
        this.dnsClient = client;
    }

    /// <inheritdoc/>
    public async Task<Result<ATDid?>> ToATDidAsync(ATIdentifier identifier)
    {
        if (identifier is ATDid atDid)
        {
            return atDid;
        }

        if (identifier is not ATHandle atHandle)
        {
            throw new NotImplementedException($"Resolving {identifier.GetType()} is not implemented.");
        }

        string didTxtRecordHost = $"_atproto.{atHandle.ToString()}";
        const string didTextRecordPrefix = "did=";
        IDnsQueryResponse dnsLookupResult = await this.dnsClient.QueryAsync(didTxtRecordHost, QueryType.TXT, QueryClass.IN, CancellationToken.None).ConfigureAwait(false);

        foreach (TxtRecord? textRecord in dnsLookupResult.Answers.TxtRecords())
        {
            foreach (string? text in textRecord.Text.Where(t => t.StartsWith(didTextRecordPrefix, StringComparison.InvariantCulture)))
            {
                if (ATDid.TryCreate(text.Substring(didTextRecordPrefix.Length), out var did))
                {
                    return did;
                }
                else
                {
                    this.logger?.LogError($"Failed to resolve Handle: {atHandle}. Invalid DID.");
                }
            }
        }

        this.logger?.LogError($"Failed to resolve Handle: {atHandle}. No valid TXT records found.");
        return null!;
    }

    /// <inheritdoc/>
    public Task<Result<ATHandle?>> ToATHandleAsync(ATIdentifier identifier)
    {
        throw new NotImplementedException();
    }
}