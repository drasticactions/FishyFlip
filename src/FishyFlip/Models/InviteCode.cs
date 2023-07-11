namespace FishyFlip.Models;

public record InviteCode(string Code, int Available, bool Disabled, AtDid ForAccount, AtDid CreatedAt, Used[] Uses);
