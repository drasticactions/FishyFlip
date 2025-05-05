// <copyright file="ATProtoModelBinderProvider.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FishyFlip.Xrpc.Tools;

/// <summary>
/// Provides a model binder for ATProto.
/// </summary>
public class ATProtoModelBinderProvider : IModelBinderProvider
{
    /// <inheritdoc/>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(ATDid))
        {
            return new ATDidModelBinder();
        }

        return null;
    }
}