// <copyright file="Globals.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

global using System.IdentityModel.Tokens.Jwt;
global using System.Net.Http.Headers;
global using System.Net.WebSockets;
global using System.Text;
global using System.Text.Encodings;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Timers;
global using FishyFlip;
global using FishyFlip.Events;
global using FishyFlip.Models;
global using FishyFlip.Models.Internal;
global using FishyFlip.Tools;
global using FishyFlip.Tools.Cbor;
global using FishyFlip.Tools.Json;
global using Ipfs;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using PeterO.Cbor;
global using ATCid = Ipfs.Cid;

#if NETSTANDARD
namespace System.Runtime.CompilerServices
{
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1649 // File name should match first type name
    internal static class IsExternalInit
    {
    }
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1600 // Elements should be documented
}
#endif