namespace FishyFlip.Lexicon;

internal static class CborExtensions
{
    /// <summary>
    /// Cast CBOR to DateTime.
    /// </summary>
    /// <param name="obj">CBORObject.</param>
    /// <returns>DateTime.</returns>
    public static DateTime? ToDateTime(this CBORObject obj)
    {
        if (obj.IsNull)
        {
            return null;
        }

        if (obj.IsNumber)
        {
            return null;
        }

        try
        {
            return DateTime.Parse(obj.AsString());
        }
        catch
        {
            return null;
        }
    }
}