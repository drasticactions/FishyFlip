namespace FishyFlip.Models;

public record InviteCodes(InviteCode[] Codes);

public record InviteCode(string Code, int Available, bool Disabled, AtDid ForAccount, AtDid CreatedAt, Used[] Uses);

public record Used(AtDid UsedBy, DateTime UsedAt);