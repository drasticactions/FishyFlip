# Custom Types

With ATProtocol, you can post, list, and delete arbitrary ATObject’s to a users PDS through the `Repo` namespace methods. However, in order to consume these, they need to be bound and available to FishyFlip. Some third-party types are bound within the library, and you can extend FishyFlip further by adding custom serialization code to allow your own types.

A full example of this is contained with `samples/CustomTypes`

First, we create our `ATObject`. We will use [statusphere-example-app](https://github.com/bluesky-social/statusphere-example-app/tree/main). It contains one unique object, [`status`](https://github.com/bluesky-social/statusphere-example-app/blob/main/lexicons/status.json).

```csharp

/// <summary>
/// Status.
/// </summary>
public class Status : ATObject
{
    /// <summary>
    /// The Record Type.
    /// </summary>
    public const string RecordType = "xyz.statusphere.status";

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <param name="status">The Status.</param>
    /// <param name="createdAt">The created date.</param>
    public Status(string status, DateTime? createdAt = default)
    {
        this.StatusValue = status;
        this.CreatedAt = createdAt ?? DateTime.UtcNow;
        this.Type = "xyz.statusphere.status";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    public Status()
    {
        this.Type = "xyz.statusphere.status";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class.
    /// </summary>
    /// <param name="obj">The CBORObject.</param>
    public Status(CBORObject obj)
    {
        if (obj["status"] is not null)
        {
            this.StatusValue = obj["status"].AsString();
        }

        if (obj["createdAt"] is not null)
        {
            this.CreatedAt = obj["createdAt"].ToDateTime();
        }
    }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonRequired]
    public string StatusValue { get; set; }

    /// <summary>
    /// Gets or sets the createdAt.
    /// </summary>
    [JsonPropertyName("createdAt")]
    [JsonRequired]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

```

Next, we create our converters. If you're using the `ATWebSocketProtocol`, you will need an `ICustomATObjectCBORConverter`. For `ATProtocol` and `ATJetStream`, we'll need an `ICustomATObjectJsonConverter`.

```csharp
/// <summary>
/// Status CBOR Converter.
/// </summary>
public class StatusCBORConverter : ICustomATObjectCBORConverter
{
    /// <inheritdoc/>
    public IReadOnlyList<string> SupportedTypes { get; } = new List<string> { Status.RecordType };

    /// <inheritdoc/>
    public ATObject? Read(CBORObject obj, string type)
    {
        return new CustomTypes.Status(obj);
    }
}
```

```csharp
/// <summary>
/// Status Converter.
/// </summary>
public class StatusConverter : ICustomATObjectJsonConverter
{
    private readonly SourceGenerationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusConverter"/> class.
    /// </summary>
    /// <param name="context">Json Type Info for StatusConverter.</param>
    internal StatusConverter(SourceGenerationContext context)
    {
        this.context = context;
        this.SupportedTypes = new List<string> { Status.RecordType };
    }

    /// <inheritdoc/>
    public IReadOnlyList<string> SupportedTypes { get; }

    /// <inheritdoc/>
    public ATObject? Read(string text, string type, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<Status>(text, this.context.Status);
    }

    /// <inheritdoc/>
    public void Write(Utf8JsonWriter writer, ATObject value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), this.context);
    }
}

/// <summary>
/// ATProtocol Message Source Generation Context.
/// </summary>
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
[JsonSerializable(typeof(Status))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
```

The `SourceGenerationContext` is not needed for `ICustomATObjectJsonConverter`, but it allows us to use Source Generation for our type, allowing for NativeAOT support. It's encouraged to avoid dynamic types for serialization or deserialization whenever possible.

Next, we add our converters. This is done through the `ATProtocolBuilder`'s

```csharp

var atProtocolBuilder = new ATProtocolBuilder([new StatusConverter(SourceGenerationContext.Default)])
    .WithLogger(debugLog.CreateLogger("FishyFlipDebug"));
var atProtocol = atProtocolBuilder.Build();

// Can share the same list of converters with ATProtocol.
var atJetStreamBuilder = new ATJustStreamBuilder([new StatusConverter(SourceGenerationContext.Default)])
    .WithLogger(debugLog.CreateLogger("FishyFlipDebugJetStream"));
var atJetStream = atJetStreamBuilder.Build();

var atWebSocketProtocolBuilder = new ATWebSocketProtocolBuilder([new StatusCBORConverter()])
    .WithLogger(debugLog.CreateLogger("FishyFlipDebugFirehose"));
var atWebSocketProtocol = atWebSocketProtocolBuilder.Build();

```

For `ATJetstream` and `ATWebSocketProtocol`, your `ATObject` records should now appear as bound objects upon getting a new message.

For `ATProtocol` you should now be able to use the `Repo` methods for handling these objects.

```csharp
var pfrazee = ATDid.Create("did:plc:ragtjsm2j2vknwkz3zp4oxrd")!;
var (listRecords, error) = await atProtocol.Repo.ListRecordsAsync(pfrazee, Status.RecordType);

if (error != null)
{
    Console.WriteLine($"Error: {error}");
    return;
}

foreach (var record in listRecords!.Records)
{
    // Records are a list of ATObject, we need to cast to get the root object.
    if (record is Status status)
    {
        Console.WriteLine($"Status: {status.StatusValue}");
    }

    // If the record is unknown, you can get the raw JSON.
    if (record is UnknownATObject unknown)
    {
        Console.WriteLine($"Unknown Type: {unknown.Type}");
        Console.WriteLine($"Unknown Type JSON: {unknown?.Json}");
    }
}
```

```csharp
var status = new Status("Hello World!");

// atProtocol.Session!.Did is the logged in users DID.
var (result, error) = await atProtocol.Repo.CreateRecordAsync(atProtocol.Session!.Did, Status.RecordType, status);

// Should return the Status
var (result2, error2) = await atProtocol.Repo.GetRecordAsync(atProtocol.Session!.Did, Status.RecordType, result.Uri.Rkey);

var status = (Status)result2.Value;

// Should say "Hello World!"
Console.WriteLine(status.StatusValue);

// Should delete the record.
var (result3, error3) = await atProtocol.Repo.DeleteRecordAsync(atProtocol.Session!.Did, Status.RecordType, result.Uri.Rkey);
```

For methods that use `OnCarDecoded`, these depend on decoding `CBORObjects`, so we will need our CBOR converter.

```csharp

// We can pass in our CBOR Converter into `ToATObject` which will include it in the deserialization.
var cborConverter = new StatusCBORConverter();
var checkoutResult = await atProtocol.Sync.GetRepoAsync(pfrazee!, HandleProgressStatus);

async void HandleProgressStatus(CarProgressStatusEvent e)
{
    var cid = e.Cid;
    var bytes = e.Bytes;
    var cborObject = CBORObject.DecodeFromBytes(bytes);

    // These objects can be Frames.
    // We can check in advance if this is an ATObject.
    if (cborObject.IsATObject())
    {
        var record = cborObject.ToATObject([cborConverter]);

        if (record is Status status)
        {
            Console.WriteLine($"Status: {status.StatusValue}");

            // Break out of the app
            Environment.Exit(0);
        }

        // If the record is unknown, you can get the raw CBORObject.
        if (record is UnknownATObject unknown)
        {
            Console.WriteLine($"Unknown Type: {unknown.Type}");
            Console.WriteLine($"Unknown Type CBORObject: {unknown?.CBORObject}");
        }
    }
}
```