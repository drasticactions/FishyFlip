// <copyright file="VerificationMethod.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a verification method used for authentication.
/// </summary>
public class VerificationMethod
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VerificationMethod"/> class.
    /// </summary>
    /// <param name="id">The identifier of the verification method.</param>
    /// <param name="type">The type of the verification method.</param>
    /// <param name="controller">The controller of the verification method.</param>
    /// <param name="publicKeyMultibase">The public key multibase of the verification method.</param>
    [JsonConstructor]
    public VerificationMethod(string id, string type, string controller, string publicKeyMultibase)
    {
        this.Id = id;
        this.Type = type;
        this.Controller = controller;
        this.PublicKeyMultibase = publicKeyMultibase;
    }

    /// <summary>
    /// Gets the identifier of the verification method.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the type of the verification method.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Gets the controller of the verification method.
    /// </summary>
    public string Controller { get; }

    /// <summary>
    /// Gets the public key multibase of the verification method.
    /// </summary>
    public string PublicKeyMultibase { get; }
}
