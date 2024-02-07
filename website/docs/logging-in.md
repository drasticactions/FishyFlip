# Logging In

- To log in, we need to create a session. This is applied to all `ATProtocol` calls once applied. If you need to create calls from a non-auth user session, create a new `ATProtocol` or destroy the existing session.

```csharp
// While this accepts normal passwords, you should ask users
// to create an app password from their accounts to use it instead.
Result<Session> result = await atProtocol.Server.CreateSessionAsync(userName, password, CancellationToken.None);

result.Switch(
    success =>
    {
        // Contains the session information and tokens used internally.
        Console.WriteLine($"Session: {success.Did}");
    },
    error =>
    {
        Console.WriteLine($"Error: {error.StatusCode} {error.Detail}");
    }
);
```

- Instead of pattern matching, you can also use `.HandleResult()` to return the `success` object, and throw an exception upon an `error`.

