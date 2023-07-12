// <copyright file="Result.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

public class Result<T> : Multiple<T, Error>
{
    public Result(T value)
        : base(0, value, default)
    {
    }

    public Result(Error value)
        : base(1, default, value)
    {
    }

    public static implicit operator Result<T>(T? t) => new(t);

    public static implicit operator Result<T>(Error? t) => new(t);
}