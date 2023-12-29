// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;
using Sharprompt;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("FishyFlipSamplesApp");

var domain = Prompt.Input<string>("Instance Domain", "https://bsky.social");
Uri.TryCreate(domain, UriKind.Absolute, out var domainUri);
if (domainUri == null)
{
    Console.WriteLine("Invalid domain.");
    return;
}

var builder = new FishyFlip.ATProtocolBuilder()
    .WithInstanceUrl(domainUri);

var authAsk = Prompt.Confirm("Do you want to authenticate?", defaultValue: false);

var atProtocol = builder.Build();

if (authAsk)
{
    var username = Prompt.Input<string>("Username");
    var password = Prompt.Password("Password");
    var authResult = (await atProtocol.Server.CreateSessionAsync(username, password)).HandleResult();
    if (authResult is null)
    {
        Console.WriteLine("Could not create auth session.");
        return;
    }
}

string[] authMenuChoices = ["Exit"];

string[] noAuthMenuChoices = ["Exit", "Get Profile Via AtDID", "Get Profile Via Handle", "Get Avatar for Profile"];

if (authAsk)
{
    while (true)
    {
        var menuChoice = Prompt.Select("Menu", authMenuChoices);
        if (menuChoice == "Exit")
        {
            break;
        }
    }
}
else
{
    while (true)
    {
        var menuChoice = Prompt.Select("Menu", noAuthMenuChoices);
        switch (menuChoice)
        {
            case "Get Avatar for Profile":
                await GetAvatarForProfile(atProtocol);
                break;
            case "Get Profile Via AtDID":
                await GetProfileViaATDID(atProtocol);
                break;
            case "Get Profile Via Handle":
                await GetProfileViaHandle(atProtocol);
                break;
            case "Exit":
                return;
        }
    }
}

async Task GetAvatarForProfile(ATProtocol protocol)
{
    var actorRecord = await GetProfileViaHandle(protocol);
    if (actorRecord is null)
    {
        return;
    }

    if (actorRecord?.Value?.Avatar is null)
    {
        Console.WriteLine("Profile has no avatar.");
        return;
    }

    // Once we have the profile record, we can get the image by using GetBlob, the actors ATDid, and the ImageRef link.
    var avatar = (await protocol.Sync.GetBlobAsync(actorRecord.Uri.Did, actorRecord.Value.Avatar.Ref.Link)).HandleResult();
    if (avatar is null)
    {
        Console.WriteLine("Could not get avatar.");
        return;
    }

    // The avatar is a byte array, so we can save it to disk.
    File.WriteAllBytes($"avatar.jpg", avatar.Data);
    Console.WriteLine("Avatar saved to disk.");

    // We can also call on the BlueSky instance to get the avatar via a URL
    var imageUri = $"https://{protocol.Options.Url.Host}{Constants.Urls.ATProtoSync.GetBlob}?did={actorRecord.Uri.Did!}&cid={actorRecord.Value.Avatar.Ref.Link}";
    Console.WriteLine($"Avatar URL: {imageUri}");
}

async Task<ActorRecord?> GetProfileViaHandle(ATProtocol protocol)
{
    var handle = Prompt.Input<string>("Handle", defaultValue: "drasticactions.dev", validators: new[] { Validators.Required() });
    var profile = (await protocol.Identity.ResolveHandleAsync(ATHandle.Create(handle)!)).HandleResult();
    return await GetProfileViaATDID(protocol, profile?.Did!);
}

async Task<ActorRecord?> GetProfileViaATDID(ATProtocol protocol, ATDid? did = null)
{
    if (did is null)
    {
        var atdid = Prompt.Input<string>("ATDID", validators: new[] { ProtocolValidators.IsATDid() });
        did = ATDid.Create(atdid);
        Console.WriteLine(did);
    }

    var profile = (await protocol.Repo.GetActorAsync(did)).HandleResult();
    Console.WriteLine($"Name: {profile?.Value?.DisplayName ?? "Empty"}");
    Console.WriteLine($"Description: {profile?.Value?.Description}" ?? "Empty");
    return profile;
}

public static class ProtocolValidators
{
    public static Func<object, ValidationResult> IsATDid()
    {
        return delegate (object input)
        {
            if (input == null)
            {
                return new ValidationResult("ATDid is invalid.");
            }

            return (input is string value && !ATDid.IsValid(value)) ? new ValidationResult("ATDid is invalid.") : ValidationResult.Success;
        };
    }
}