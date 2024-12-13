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
    /// <param name="statusCode">The status code.</param>
    public ATError(int statusCode)
    {
        this.StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    /// <param name="message">The message.</param>
    public ATError(int statusCode, string? message)
    {
        this.StatusCode = statusCode;
        this.Detail = new ErrorDetail(message ?? string.Empty, string.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    /// <param name="message">The message.</param>
    /// <param name="messageDetail">The detail.</param>
    public ATError(int statusCode, string? message, string? messageDetail)
    {
        this.StatusCode = statusCode;
        this.Detail = new ErrorDetail(message ?? string.Empty, messageDetail ?? string.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    /// <param name="detail">The detail.</param>
    public ATError(ErrorDetail? detail)
    {
        this.StatusCode = 0;
        this.Detail = detail;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ATError"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public ATError(string message)
    {
        this.StatusCode = 0;
        this.Detail = new ErrorDetail(message, string.Empty);
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