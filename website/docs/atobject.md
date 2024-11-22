# `ATObject`

`ATObject` is the base level object for items sent and returned from `ATProtocol`. When defined by the protocol to return a single type, FishyFlip will bind to that type. However, some return types are `union` (consisting of several possible return types that are defined) or `unknown`, which is not defined.

In these cases, the return object is the base `ATObject`, and can be cast to get the object you want. For example:

```csharp
var postUri = ATUri.Create("at://did:plc:oio4hkxaop4ao4wz2pp3f4cr/app.bsky.feed.post/3lbhrggre422j");
var result = (await atProtocol.Feed.GetPostThreadAsync(uri: postUri)).HandleResult();

// Thread is defined as a union in the lexicon, with the types "#threadViewPost", "#notFoundPost", and "#blockedPost"
// These are bound as ThreadViewPost, NotFoundPost, and BlockedPost respectively.
switch (result?.Thread)
{
  case ThreadViewPost tvp:
   Console.WriteLine($"ThreadViewPost: {tvp}");
   // tvp.Replies is a List<ATObject?>. These are the same union type as Thread,
   // And can return a ThreadViewPost, NotFoundPost, or BlockedPost.

   // Record is an "unknown" type in the lexicon, which is ambiguous.
   // But is generally known to be a Post.
   switch (tvp.Post?.Record)
   {
    case Post post:
     Console.WriteLine($"Post: {post.Text}");
     break;
    default:
     Console.WriteLine("Unknown");
     break;
   }

   break;
  case NotFoundPost nfp:
   Console.WriteLine($"NotFoundPost: {nfp}");
   break;
  case BlockedPost bp:
   Console.WriteLine($"BlockedPost: {bp}");
   break;
  default:
   // If we get this, we have a problem.
   // This should never happen.
   Console.WriteLine("Unknown");
   break;
}
```

For `union` types, the known values for what is either returned or accepted for each parameter should be in the documentation, either in the Lexicon or in the FishyFlip object documentation. If it's not listed in these docs, please file an issue so we can define it.

**NOTE**: While it's tempting to directly cast an `ATObject` to a type, you should avoid it and instead either switch on the type or use nullable casts to verify the results. Consult the ATProtocol Lexicon for what should be returned.