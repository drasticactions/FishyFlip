// <copyright file="ATDidModelBinder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FishyFlip.Xrpc.Tools;

/// <summary>
/// Model binder for ATDid.
/// </summary>
public class ATDidModelBinder : IModelBinder
{
    /// <inheritdoc/>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        try
        {
            var atdid = ATDid.Create(value);
            bindingContext.Result = ModelBindingResult.Success(atdid);
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
        }

        return Task.CompletedTask;
    }
}