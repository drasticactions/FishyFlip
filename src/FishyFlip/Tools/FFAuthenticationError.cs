// <copyright file="FFAuthenticationError.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace FishyFlip.Tools;

/// <summary>
/// Thrown if the user is not authenticated and is trying to use an authenticated endpoint.
/// </summary>
internal class FFAuthenticationError : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FFAuthenticationError"/> class.
    /// </summary>
    public FFAuthenticationError()
        : base("User is not authenticated. Create a PasswordSession or OAuth2Session before using this method.")
    {
    }
}
