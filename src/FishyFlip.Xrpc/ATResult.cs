// <copyright file="ATResult.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace FishyFlip.Models;

/// <summary>
/// Represents the result of an ATProto XRPC operation.
/// </summary>
/// <typeparam name="T">The type of the result data.</typeparam>
public class ATResult<T> : IResult
    where T : ATObject
{
    private int statusCode = 200;

    private T? data;

    /// <summary>
    /// Ok result with status code 200 and at ATObject.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns><see cref="ATResult"/>.</returns>
    public static ATResult<T> Ok(T? data)
    {
        return new ATResult<T>
        {
            statusCode = 200,
            data = data,
        };
    }

    /// <inheritdoc/>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = this.statusCode;
        if (this.data != null)
        {
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(this.data.ToJson());
        }

        return Task.CompletedTask;
    }
}