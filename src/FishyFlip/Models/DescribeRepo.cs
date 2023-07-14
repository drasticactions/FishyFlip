// <copyright file="DescribeRepo.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public class DescribeRepo
{
    [JsonConstructor]
    public DescribeRepo(string handle, ATDid did, DidDoc didDoc, List<object> collections, bool handleIsCorrect)
    {
        this.Handle = handle;
        this.Did = did;
        this.DidDoc = didDoc;
        this.Collections = collections;
        this.HandleIsCorrect = handleIsCorrect;
    }

    public string Handle { get; }

    public ATDid Did { get; }

    public DidDoc DidDoc { get; }

    public List<object> Collections { get; }

    public bool HandleIsCorrect { get; }
}

public class DidDoc
{
    [JsonConstructor]
    public DidDoc(List<string> context, string id, List<string> alsoKnownAs,
                  List<VerificationMethod> verificationMethod, List<Service> service)
    {
        this.Context = context;
        this.Id = id;
        this.AlsoKnownAs = alsoKnownAs;
        this.VerificationMethod = verificationMethod;
        this.Service = service;
    }

    public List<string> Context { get; }

    public string Id { get; }

    public List<string> AlsoKnownAs { get; }

    public List<VerificationMethod> VerificationMethod { get; }

    public List<Service> Service { get; }
}

public class VerificationMethod
{
    [JsonConstructor]
    public VerificationMethod(string id, string type, string controller, string publicKeyMultibase)
    {
        this.Id = id;
        this.Type = type;
        this.Controller = controller;
        this.PublicKeyMultibase = publicKeyMultibase;
    }

    public string Id { get; }

    public string Type { get; }

    public string Controller { get; }

    public string PublicKeyMultibase { get; }
}

public class Service
{
    [JsonConstructor]
    public Service(string id, string type, string serviceEndpoint)
    {
        this.Id = id;
        this.Type = type;
        this.ServiceEndpoint = serviceEndpoint;
    }

    public string Id { get; }

    public string Type { get; }

    public string ServiceEndpoint { get; }
}
