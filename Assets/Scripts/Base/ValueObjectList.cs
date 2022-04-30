using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

public sealed class ValueObjectList<T>
{
    public ValueObjectList()
    {
        Items = ImmutableList<T>.Empty;
    }

    public ValueObjectList([NotNull] [ItemNotNull] ImmutableList<T> items)
    {
        Items = items;
    }

    public ValueObjectList([NotNull] [ItemNotNull] params T[] items)
    {
        Items = items.ToImmutableList();
    }

    public ValueObjectList([NotNull] [ItemNotNull] IEnumerable<T> items)
    {
        Items = items.ToImmutableList();
    }

    [NotNull] [ItemNotNull] public ImmutableList<T> Items { get; }

    public static ValueObjectList<T> Empty => new();

    public override bool Equals(object obj)
    {
        return obj is ValueObjectList<T> other ? Equals(other) : false;
    }

    private bool Equals(ValueObjectList<T> other)
    {
        return Items.SequenceEqual(other.Items);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return Items.Aggregate(0, (agg, curr) => (agg * 397) ^ curr.GetHashCode());
        }
    }

    public static bool operator ==(ValueObjectList<T> item1, ValueObjectList<T> item2)
    {
        return !(item1 is null) && item1.Equals(item2);
    }

    public static bool operator !=(ValueObjectList<T> item1, ValueObjectList<T> item2)
    {
        return !(item1 == item2);
    }
}

public static class ValueObjectList
{
    public static ValueObjectList<T> ToValueObjectList<T>(this IEnumerable<T> source)
    {
        return new ValueObjectList<T>(source.ToImmutableList());
    }

    public static ValueObjectList<T> ToValueObjectList<T>(this ImmutableList<T> source)
    {
        return new ValueObjectList<T>(source);
    }

    public static ValueObjectList<T> Create<T>(T item)
    {
        return ImmutableList.Create(item).ToValueObjectList();
    }

    public static ValueObjectList<T> Create<T>(params T[] items)
    {
        return ImmutableList.Create(items).ToValueObjectList();
    }
}