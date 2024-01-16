// <copyright file="Multiple.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using static FishyFlip.Tools.StringExtensions;

namespace FishyFlip.Tools;

/// <summary>
/// Minimalistic https://github.com/mcintyre321/OneOf.
/// </summary>
/// <typeparam name="T0">First type.</typeparam>
/// <typeparam name="T1">Second Type.</typeparam>
public class Multiple<T0, T1>
{
    private readonly T0? value0;
    private readonly T1? value1;
    private readonly int index;

    public Multiple(int index, T0? value0 = default, T1? value1 = default)
    {
        this.index = index;
        this.value0 = value0;
        this.value1 = value1;
    }

    public object? Value =>
        this.index switch
        {
            0 => this.value0,
            1 => this.value1,
            _ => throw new InvalidOperationException(),
        };

    public int Index => this.index;

    public bool IsT0 => this.index == 0;

    public bool IsT1 => this.index == 1;

    public T0 AsT0 =>
        this.index == 0 ? this.value0! : throw new InvalidOperationException($"Cannot return as T0 as result is T{this.index}");

    public T1 AsT1 =>
        this.index == 1 ? this.value1! : throw new InvalidOperationException($"Cannot return as T1 as result is T{this.index}");

    public static implicit operator Multiple<T0?, T1>(T0? t) => new Multiple<T0?, T1>(0, value0: t);

    public static implicit operator Multiple<T0, T1?>(T1? t) => new Multiple<T0, T1?>(1, value1: t);

#nullable disable
    public static Multiple<T0, T1> FromT0(T0 input) => input;

    public static Multiple<T0, T1> FromT1(T1 input) => input;
#nullable enable

    public async Task SwitchAsync(Func<T0, Task> f0, Func<T1, Task> f1)
    {
        switch (this.index)
        {
            case 0:
                await f0(this.value0!);
                return;
            case 1:
                await f1(this.value1!);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public void Switch(Action<T0> f0, Action<T1> f1)
    {
        switch (this.index)
        {
            case 0:
                f0(this.value0!);
                return;
            case 1:
                f1(this.value1!);
                return;
            default:
                throw new InvalidOperationException();
        }
    }

    public TResult Match<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1)
    {
        return this.index switch
        {
            0 => f0(this.value0!),
            1 => f1(this.value1!),
            _ => throw new InvalidOperationException(),
        };
    }

    public async Task<TResult> MatchAsync<TResult>(Func<T0, Task<TResult>> f0, Func<T1, Task<TResult>> f1)
    {
        return this.index switch
        {
            0 => await f0(this.value0!),
            1 => await f1(this.value1!),
            _ => throw new InvalidOperationException(),
        };
    }

    public Multiple<TResult, T1> MapT0<TResult>(Func<T0, TResult> mapFunc)
    {
        return this.index switch
        {
            0 => mapFunc.ThrowIfNull()(this.AsT0!)!,
            1 => this.AsT1!,
            _ => throw new InvalidOperationException(),
        };
    }

    public Multiple<T0, TResult> MapT1<TResult>(Func<T1, TResult> mapFunc)
    {
        if (mapFunc == null)
        {
            throw new ArgumentNullException(nameof(mapFunc));
        }

        return this.index switch
        {
            0 => this.AsT0!,
            1 => mapFunc(this.AsT1!)!,
            _ => throw new InvalidOperationException(),
        };
    }

    public bool TryPickT0(out T0? value, out T1? remainder)
    {
        value = this.IsT0 ? this.AsT0! : default!;
        remainder = this.index switch
        {
            0 => default!,
            1 => this.AsT1!,
            _ => throw new InvalidOperationException(),
        };
        return this.IsT0;
    }

    public bool TryPickT1(out T1? value, out T0? remainder)
    {
        value = this.IsT1 ? this.AsT1! : default!;
        remainder = this.index switch
        {
            0 => this.AsT0!,
            1 => default!,
            _ => throw new InvalidOperationException(),
        };
        return this.IsT1;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is Multiple<T0, T1> o && this.Equals(o!);
    }

    /// <inheritdoc/>
    public override string ToString() =>
        this.index switch
        {
            0 => FormatValue(this.value0),
            1 => FormatValue(this.value1),
            _ => throw new InvalidOperationException(
                "Unexpected index, which indicates a problem in the OneOf codegen."),
        };

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = this.index switch
            {
                0 => this.value0?.GetHashCode(),
                1 => this.value1?.GetHashCode(),
                _ => 0,
            }

?? 0;
            return (hashCode * 397) ^ this.index;
        }
    }

    private bool Equals(Multiple<T0, T1?> other) =>
        this.index == other.index &&
        this.index switch
        {
            0 => Equals(this.value0, other.value0),
            1 => Equals(this.value1, other.value1),
            _ => false,
        };
}