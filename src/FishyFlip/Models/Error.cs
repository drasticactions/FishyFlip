namespace FishyFlip.Models;

public record Error(int StatusCode, ErrorDetail? Detail = default)
{
}