// <copyright file="Result.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a result that can either contain a value of type <typeparamref name="T"/> or an error of type <see cref="Error"/>.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class Result<T> : Multiple<T, Error>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public Result(T value)
        : base(0, value, default)
    {
    }

/// <summary>
/// Initializes a new instance of the <see cref="Result{T}"/> class.
/// </summary>
/// <param name="value">Represents the type of value.</param>
    public Result(Error? value)
        : base(1, default, value)
    {
    }

    public static implicit operator Result<T?>(T? t) => new(t);

    public static implicit operator Result<T?>(Error? t) => new(t);
}