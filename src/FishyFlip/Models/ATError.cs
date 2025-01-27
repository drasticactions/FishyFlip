// <copyright file="ATError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// ATProtocol Error Response.
/// Returned when an error occurs in the ATProtocol.
/// </summary>
public class ATError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    public ATError()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    /// <param name="detail">The detail.</param>
    public ATError(int statusCode, ErrorDetail? detail)
    {
        this.StatusCode = statusCode;
        this.Detail = detail;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>ATError.</returns>
    public ATError(Exception exception)
    {
        this.StatusCode = 500;
        this.Detail = new ErrorDetail()
        {
            Message = exception.Message,
            StackTrace = exception.StackTrace,
        };
    }

    /// <summary>
    /// Gets or sets the status code.
    /// </summary>
    [JsonPropertyName("statuscode")]
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the atError detail.
    /// </summary>
    [JsonPropertyName("detail")]
    public ErrorDetail? Detail { get; set; }

    /// <summary>
    /// ToString override.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return $"StatusCode: {this.StatusCode}, {this.Detail}";
    }
}