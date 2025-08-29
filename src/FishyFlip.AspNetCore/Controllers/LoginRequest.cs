// <copyright file="LoginRequest.cs" company="Drastic Actions">
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
/// Request model for password login.
/// </summary>
/// <param name="Identifier">User identifier (handle or email).</param>
/// <param name="Password">User password.</param>
/// <param name="InstanceUrl">Optional instance URL.</param>
public record LoginRequest(string Identifier, string Password, string? InstanceUrl = null);
