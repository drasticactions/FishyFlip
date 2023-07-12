namespace FishyFlip.Models;

public record InviteCode(string Code, int Available, bool Disabled, ATDid ForAccount, ATDid CreatedAt, Used[] Uses);
