// <copyright file="SchemaDocumentDefinition.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FFSourceGen.Models;

public class SchemaDocumentDefinition(SchemaDocument schema, SchemaDefinition definition)
{
    public SchemaDocument Schema => schema;

    public SchemaDefinition Definition => definition;
}