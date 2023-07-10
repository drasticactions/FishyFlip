namespace FishyFlip.Tools.Cbor;

/// <summary>
/// Frame header converter.
/// </summary>
public class FrameHeaderConverter : ICBORToFromConverter<FrameHeader>
{
    /// <inheritdoc/>
    public CBORObject ToCBORObject(FrameHeader cpod)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    /// <inheritdoc/>
    public FrameHeader FromCBORObject(CBORObject obj)
    {
        if (obj.Type != CBORType.Map)
        {
            throw new CBORException();
        }

        return new FrameHeader
        {
            Operation = (FrameHeaderOperation)(obj["op"]?.AsInt32() ?? 0),
            Type = obj["t"].AsString(),
        };
    }
}