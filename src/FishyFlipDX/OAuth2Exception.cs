// <copyright file="OAuth2Exception.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip;

/// <summary>
/// Message thrown when an OAuth2 Exception occurs.
/// </summary>
public class OAuth2Exception : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2Exception"/> class.
    /// </summary>
    /// <param name="message">Message.</param>
    public OAuth2Exception(string message)
        : base(message)
    {
    }
}
