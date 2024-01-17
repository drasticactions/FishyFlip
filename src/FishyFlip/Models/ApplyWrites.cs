namespace FishyFlip.Models;

/// <summary>
/// Apply a batch transaction of creates, updates, and deletes.
/// </summary>
/// <param name="Repo">The handle or DID of the repo.</param>
/// <param name="Writes">Array of Write Actions.</param>
/// <param name="Validate">Flag for validating the records.</param>
/// <param name="SwapCommit">Swap the commit with another CID.</param>
public record ApplyWrites(ATDid Repo, ApplyWriteRecord[] Writes, bool Validate = true, string? SwapCommit = default);

public class CreateRecord : ApplyWriteRecord
{
    [JsonConstructor]
    public CreateRecord(string collection, string rkey, ATRecord record)
        : base(collection, rkey)
    {
        this.Type = Constants.ApplyWriteTypes.Create;
        this.Record = record;
    }

    public ATRecord Record { get; }
}

public class DeleteRecord : ApplyWriteRecord
{
    [JsonConstructor]
    public DeleteRecord(string collection, string rkey)
        : base(collection, rkey)
    {
        this.Type = Constants.ApplyWriteTypes.Delete;
    }
}

public class UpdateRecord : ApplyWriteRecord
{
    [JsonConstructor]
    public UpdateRecord(string collection, string rkey, ATRecord record)
        : base(collection, rkey)
    {
        this.Type = Constants.ApplyWriteTypes.Update;
        this.Record = record;
    }

    public ATRecord Record { get; }
}

public class ApplyWriteRecord : ATRecord
{
    [JsonConstructor]
    public ApplyWriteRecord(string collection, string rkey)
    {
        this.Collection = collection;
        this.RKey = rkey;
    }

    public string Collection { get; }

    public string RKey { get; }
}