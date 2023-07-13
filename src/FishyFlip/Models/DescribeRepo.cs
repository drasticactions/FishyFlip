using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public class DescribeRepo
{
    public string Handle { get; }
    public ATDid Did { get; }
    public DidDoc DidDoc { get; }
    public List<object> Collections { get; }
    public bool HandleIsCorrect { get; }

    [JsonConstructor]
    public DescribeRepo(string handle, ATDid did, DidDoc didDoc, List<object> collections, bool handleIsCorrect)
    {
        Handle = handle;
        Did = did;
        DidDoc = didDoc;
        Collections = collections;
        HandleIsCorrect = handleIsCorrect;
    }
}

public class DidDoc
{
    public List<string> Context { get; }
    public string Id { get; }
    public List<string> AlsoKnownAs { get; }
    public List<VerificationMethod> VerificationMethod { get; }
    public List<Service> Service { get; }

    [JsonConstructor]
    public DidDoc(List<string> context, string id, List<string> alsoKnownAs,
                  List<VerificationMethod> verificationMethod, List<Service> service)
    {
        Context = context;
        Id = id;
        AlsoKnownAs = alsoKnownAs;
        VerificationMethod = verificationMethod;
        Service = service;
    }
}

public class VerificationMethod
{
    public string Id { get; }
    public string Type { get; }
    public string Controller { get; }
    public string PublicKeyMultibase { get; }

    [JsonConstructor]
    public VerificationMethod(string id, string type, string controller, string publicKeyMultibase)
    {
        Id = id;
        Type = type;
        Controller = controller;
        PublicKeyMultibase = publicKeyMultibase;
    }
}

public class Service
{
    public string Id { get; }
    public string Type { get; }
    public string ServiceEndpoint { get; }

    [JsonConstructor]
    public Service(string id, string type, string serviceEndpoint)
    {
        Id = id;
        Type = type;
        ServiceEndpoint = serviceEndpoint;
    }
}
