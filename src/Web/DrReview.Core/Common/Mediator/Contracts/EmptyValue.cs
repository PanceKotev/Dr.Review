namespace DrReview.Common.Mediator.Contracts;

using System;
using System.Threading.Tasks;

/// <summary>
/// An empty value to return, since we cannot return void.
/// <br/><br/>
/// All comparison results return their default value since this struct has no value.
/// </summary>
public readonly struct EmptyValue : IEquatable<EmptyValue>, IComparable<EmptyValue>, IComparable
{
    private static readonly EmptyValue _value = new ();

    /// <summary>
    /// Gets the empty value of the property.
    /// </summary>
    /// <value>
    /// Empty.
    /// </value>
    public static ref readonly EmptyValue Value => ref _value;

    /// <summary>
    /// Gets a task with this empty value.
    /// </summary>
    /// <value>Task with empty value.</value>
    public static Task<EmptyValue> Task { get; } = System.Threading.Tasks.Task.FromResult(_value);

    public static bool operator ==(EmptyValue first, EmptyValue second)
    {
        return first.CompareTo(second) == 0;
    }

    public static bool operator !=(EmptyValue first, EmptyValue second)
    {
        return first.CompareTo(second) != 0;
    }

    public static bool operator <(EmptyValue left, EmptyValue right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(EmptyValue left, EmptyValue right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(EmptyValue left, EmptyValue right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(EmptyValue left, EmptyValue right)
    {
        return left.CompareTo(right) >= 0;
    }

    public int CompareTo(EmptyValue other) => 0;

    int IComparable.CompareTo(object? obj) => 0;

    public override int GetHashCode() => 0;

    public bool Equals(EmptyValue other) => true;

    public override bool Equals(object? obj) => obj is EmptyValue;

    public override string ToString() => string.Empty;
}
