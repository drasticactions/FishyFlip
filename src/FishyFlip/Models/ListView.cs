namespace FishyFlip.Models;

public record ListView(ATUri Uri, Cid Cid, string Name, string Purpose, string Description, ActorProfile Creator, Viewer Viewer, DateTime IndexedAt);

public record ListViewRecord(ListView[] Lists, string? Cursor);

public record ListItemView(ATUri Uri, ActorProfile Subject);

public record ListItemViewRecord(ListItemView[] Items, ListView List, string? Cursor);