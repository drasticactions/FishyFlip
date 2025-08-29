// <copyright file="SessionInfo.cs" company="Drastic Actions">
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
/// Session information model.
/// </summary>
/// <param name="Did">User's DID.</param>
/// <param name="Handle">User's handle.</param>
/// <param name="Email">User's email.</param>
public record SessionInfo(string? Did, string? Handle, string? Email);