namespace FishyFlip.Commands;

public record GetAccountInviteCodes(bool IncludeUsed = true, bool CreateAvailable = true);