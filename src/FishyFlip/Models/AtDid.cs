using FishyFlip.Exceptions;
using FishyFlip.Tools;

namespace FishyFlip.Models;

public class AtDid
{
    protected AtDid(string ident)
    {
        this.Handler = ident;
    }

    public string Handler { get; }

    public override string ToString()
    {
        return this.Handler.ToString();
    }

    internal static AtDid? Create(AtUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        try
        {
            DIDValidator.EnsureValidDid(uri.Hostname);
            return new AtDid(uri.Hostname);
        }
        catch (InvalidDidError)
        {
        }

        return null;
    }
}