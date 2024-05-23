// <copyright file="LogResponse.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Response to a log request.
/// </summary>
/// <param name="Cursor">A cursor that can be used to paginate through logs.</param>
/// <param name="Logs">Logs.</param>
public record LogResponse(string Cursor, ATRecord[] Logs);