// <copyright file="DescribeRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Represents a service.
/// </summary>
public class Service
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Service"/> class.
    /// </summary>
    /// <param name="id">The ID of the service.</param>
    /// <param name="type">The type of the service.</param>
    /// <param name="serviceEndpoint">The service endpoint.</param>
    [JsonConstructor]
    public Service(string id, string type, string serviceEndpoint)
    {
        this.Id = id;
        this.Type = type;
        this.ServiceEndpoint = serviceEndpoint;
    }

    /// <summary>
    /// Gets the ID of the service.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the type of the service.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Gets the service endpoint.
    /// </summary>
    public string ServiceEndpoint { get; }
}
