using FishyFlip.Exceptions;
using FishyFlip.Tools;

namespace FishyFlip.Models;

public class AtHandler
{
    protected AtHandler(string ident)
    {
       this.Handler = ident;
    }

    public string Handler { get; }

    public override string ToString()
    {
        return this.Handler.ToString();
    }

    internal static AtHandler? Create(AtUri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        try
        {
            HandleValidator.EnsureValidHandle(uri.Hostname);
            return new AtHandler(uri.Hostname);
        }
        catch (InvalidHandleError)
        {
        }

        return null;
    }
}