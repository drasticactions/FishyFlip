// <copyright file="LoginResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FishyFlip.AspNetCore.Controllers;

/// <summary>
/// Response model for successful login.
/// </summary>
/// <param name="SessionId">The session identifier.</param>
/// <param name="Did">User's DID.</param>
/// <param name="Handle">User's handle.</param>
public record LoginResponse(string SessionId, string Did, string Handle);
