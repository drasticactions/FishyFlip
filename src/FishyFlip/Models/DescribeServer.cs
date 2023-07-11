namespace FishyFlip.Models;

public record DescribeServer(string[] AvailableUserDomains, bool InviteCodeRequired, ServerLinkProperties Links);
