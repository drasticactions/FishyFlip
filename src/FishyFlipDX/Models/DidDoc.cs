// <copyright file="DidDoc.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// DidDoc.
/// Represents a Decentralized Identifier Document upon login.
/// </summary>
public class DidDoc
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DidDoc"/> class.
    /// </summary>
    /// <param name="context">Contexts.</param>
    /// <param name="id">Id.</param>
    /// <param name="alsoKnownAs">Also known as values.</param>
    /// <param name="verificationMethod">Verification methods.</param>
    /// <param name="service">List of services.</param>
    [JsonConstructor]
    public DidDoc(
        List<string> context,
        string id,
        List<string> alsoKnownAs,
        List<VerificationMethod> verificationMethod,
        List<Service> service)
    {
        this.Context = context;
        this.Id = id;
        this.AlsoKnownAs = alsoKnownAs;
        this.VerificationMethod = verificationMethod;
        this.Service = service;
    }

    /// <summary>
    /// Gets the list of contexts.
    /// </summary>
    [JsonPropertyName("@context")]
    public List<string> Context { get; }

    /// <summary>
    /// Gets the Id.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the list of also known as for the given Did.
    /// </summary>
    public List<string> AlsoKnownAs { get; }

    /// <summary>
    /// Gets the list of verification methods.
    /// </summary>
    public List<VerificationMethod> VerificationMethod { get; }

    /// <summary>
    /// Gets the list of services.
    /// </summary>
    public List<Service> Service { get; }

    /// <inheritdoc/>
    public override string ToString()
    {
        var alsoKnownAsNamesAsString = string.Join(", ", this.AlsoKnownAs);
        return $"ID: {this.Id}, AlsoKnownAs: {alsoKnownAsNamesAsString}";
    }
}
