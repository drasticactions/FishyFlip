namespace FishyFlip.Models;

public record AppPasswords(AppPassword[] Passwords);

public record AppPassword(string Name, DateTime CreatedAt);