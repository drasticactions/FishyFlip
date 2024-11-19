// <copyright file="Result.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a result that can either contain a value of type <typeparamref name="T"/> or an atError of type <see cref="ATError"/>.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class Result<T> : Multiple<T, ATError>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">Represents the type of value.</param>
    private Result(T value)
        : base(0, value, default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">Represents the type of value.</param>
    private Result(ATError? value)
        : base(1, default, value)
    {
    }

    public static implicit operator Result<T?>(T? t) => new(t);

    public static implicit operator Result<T?>(ATError? t) => new(t);
}