// <copyright file="FrameBodyConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Cbor;

/// <summary>
/// Frame body converter.
/// </summary>
public class FrameBodyConverter : ICBORToFromConverter<FrameBody>
{
    private readonly CBORTypeMapper mapper;
    public FrameBodyConverter(OpsConverter opsConverter)
    {
        this.mapper = new CBORTypeMapper()
            .AddConverter(typeof(Ops), opsConverter);
    }

    /// <inheritdoc/>
    public CBORObject ToCBORObject(FrameBody cpod)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    /// <inheritdoc/>
    public FrameBody FromCBORObject(CBORObject obj)
    {
        if (obj.Type != CBORType.Map)
        {
            throw new CBORException();
        }

        var ret = new FrameBody();
        ret.Ops = obj["ops"].Values.Select(n => n.ToObject<Ops>(this.mapper)).ToArray();
        ret.Seq = obj["seq"].AsInt32();
        ret.Blocks = obj["blocks"].GetByteString();
        return ret;
    }
}