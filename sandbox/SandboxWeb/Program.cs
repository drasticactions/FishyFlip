// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

/*
 * This is a sandbox app for testing FishyFlip functions.
 */

using System.Text.Json.Serialization;
using FishyFlip.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);

// Register controllers
builder.Services.AddControllers(options =>
{
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
});

var app = builder.Build();

app.MapGet("/", () => "Hello, Sandbox!");

// Map controller endpoints
app.MapControllers();

app.Run();