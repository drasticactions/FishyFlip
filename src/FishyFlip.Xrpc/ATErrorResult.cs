// <copyright file="ATErrorResult.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http;

namespace FishyFlip.Models;

/// <summary>
/// Error Result.
/// </summary>
public class ATErrorResult : ATError, IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATErrorResult"/> class.
    /// </summary>
    public ATErrorResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATErrorResult"/> class.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>ATError.</returns>
    public ATErrorResult(Exception exception)
    {
        this.StatusCode = 500;
        this.Detail = new ErrorDetail()
        {
            Message = exception.Message,
            StackTrace = exception.StackTrace,
        };
    }

    /// <summary>
    /// Creates a Not Found 404 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult NotFound(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 404,
            Detail = new ErrorDetail
            {
                Error = "NotFound",
                Message = message ?? "Not Found",
            },
        };
    }

    /// <summary>
    /// Creates a Bad Request 400 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult BadRequest(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 400,
            Detail = new ErrorDetail
            {
                Error = "BadRequest",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Bad Request 401 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult Unauthorized(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 401,
            Detail = new ErrorDetail
            {
                Error = "AuthMissing",
                Message = message ?? "Authentication Required",
            },
        };
    }

    /// <summary>
    /// Creates an instance of <see cref="ATErrorResult"/> from an exception.
    /// Returns a 500 status code with the exception message and stack trace.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult InternalServerError(Exception exception)
    {
        return new ATErrorResult
        {
            StatusCode = 500,
            Detail = new ErrorDetail
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
            },
        };
    }

    /// <summary>
    /// Creates a Internal Service Error 500 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult InternalServerError(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 500,
            Detail = new ErrorDetail
            {
                Error = "InternalServerError",
                Message = message ?? "Internal Server Error",
            },
        };
    }

    /// <summary>
    /// Creates a Forbidden 403 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult Forbidden(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 403,
            Detail = new ErrorDetail
            {
                Error = "Forbidden",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Payload Too Large 413 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult PayloadTooLarge(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 413,
            Detail = new ErrorDetail
            {
                Error = "PayloadTooLarge",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Too Many Requests 429 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    public static ATErrorResult TooManyRequests(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 429,
            Detail = new ErrorDetail
            {
                Error = "TooManyRequests",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Not Implemented 501 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    /// <remarks>
    /// This is used when the server does not support the functionality required to fulfill the request.
    /// </summary>
    public static ATErrorResult NotImplemented(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 501,
            Detail = new ErrorDetail
            {
                Error = "NotImplemented",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Bad Gateway 502 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    /// <remarks>
    /// This is used when the server, while acting as a gateway or proxy, received an invalid response from the upstream server.
    /// </remarks>
    public static ATErrorResult BadGateway(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 502,
            Detail = new ErrorDetail
            {
                Error = "BadGateway",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Service Unavailable 503 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    /// <remarks>
    /// This is used when the server is currently unable to handle the request due to temporary overloading or maintenance of the server.
    /// </remarks>
    public static ATErrorResult ServiceUnavailable(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 503,
            Detail = new ErrorDetail
            {
                Error = "ServiceUnavailable",
                Message = message,
            },
        };
    }

    /// <summary>
    /// Creates a Gateway Timeout 504 Error.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns><see cref="ATErrorResult"/>.</returns>
    /// <remarks>
    /// This is used when the server, while acting as a gateway or proxy, did not receive a timely response from the upstream server.
    /// </remarks>
    public static ATErrorResult GatewayTimeout(string? message = default)
    {
        return new ATErrorResult
        {
            StatusCode = 504,
            Detail = new ErrorDetail
            {
                Error = "GatewayTimeout",
                Message = message,
            },
        };
    }

    /// <inheritdoc/>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = this.StatusCode;
        httpContext.Response.ContentType = "application/json";
        var internalError = new XrpcError
        {
            Error = this.Detail?.Error,
            Message = this.Detail?.Message,
        };
        return httpContext.Response.WriteAsync(internalError.ToJson());
    }
}