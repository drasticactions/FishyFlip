namespace FishyFlip.Models;

public class Blob
{
    public Blob(byte[]? data)
    {
        this.Data = data;
    }

    public byte[]? Data { get; }
}
