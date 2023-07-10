// <copyright file="OpsConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Tools.Cbor;

/// <summary>
/// Ops converter.
/// </summary>
public class OpsConverter : ICBORToFromConverter<Ops>
{
    /// <inheritdoc/>
    public CBORObject ToCBORObject(Ops cpod)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    /// <inheritdoc/>
    public Ops FromCBORObject(CBORObject obj)
    {
        if (obj.Type != CBORType.Map)
        {
            throw new CBORException();
        }

        var ret = new Ops();
        return ret;
    }
}