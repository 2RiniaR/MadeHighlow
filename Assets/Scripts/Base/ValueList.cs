using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

public sealed class ValueList<T> : IEnumerable<T>
{
    public ValueList()
    {
        Items = ImmutableList<T>.Empty;
    }

    public ValueList([NotNull] [ItemNotNull] ImmutableList<T> items)
    {
        Items = items;
    }

    public ValueList([NotNull] [ItemNotNull] params T[] items)
    {
        Items = items.ToImmutableList();
    }

    public ValueList([NotNull] [ItemNotNull] IEnumerable<T> items)
    {
        Items = items.ToImmutableList();
    }

    [NotNull] [ItemNotNull] private ImmutableList<T> Items { get; }

    [NotNull] [ItemNotNull] public static ValueList<T> Empty => new();

    public int Count => Items.Count;
    public bool IsEmpty => Items.IsEmpty;

    public T this[int index] => Items[index];

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object obj)
    {
        return obj is ValueList<T> other ? Equals(other) : false;
    }

    private bool Equals(ValueList<T> other)
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

    public override string ToString()
    {
        return "[" + Items.Aggregate("", (current, item) => current + ", " + item) + "]";
    }

    public ValueList<T> ReplaceItem(Predicate<T> predicate, T newItem)
    {
        var index = Items.FindIndex(predicate);
        return Items.SetItem(index, newItem).ToValueList();
    }

    [CanBeNull]
    public T Find(Predicate<T> predicate)
    {
        return Items.Find(predicate);
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<T> Where(Predicate<T> predicate)
    {
        return Items.FindAll(predicate).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<T> Add([NotNull] T item)
    {
        return Items.Add(item).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<T> AddRange([NotNull] [ItemNotNull] IEnumerable<T> items)
    {
        return Items.AddRange(items).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<TResult> SelectMany<TResult>(Func<T, IEnumerable<TResult>> selector)
    {
        return Items.SelectMany(selector).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<TResult> Cast<TResult>()
    {
        return Items.Cast<TResult>().ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<TResult> WhereType<TResult>() where TResult : class
    {
        return Items.Select(item => item as TResult).Where(item => item != null).ToValueList();
    }

    [NotNull]
    public ValueList<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        return Items.Select(selector).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<T> RemoveNull()
    {
        return Items.FindAll(item => item != null).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<T> Sort(Comparison<T> comparison)
    {
        return Items.Sort(comparison).ToValueList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueList<T> Sort()
    {
        return Items.Sort().ToValueList();
    }

    public TResult Aggregate<TResult>(TResult seed, Func<TResult, T, TResult> func)
    {
        return Items.Aggregate(seed, func);
    }

    public static bool operator ==(ValueList<T> item1, ValueList<T> item2)
    {
        return !(item1 is null) && item1.Equals(item2);
    }

    public static bool operator !=(ValueList<T> item1, ValueList<T> item2)
    {
        return !(item1 == item2);
    }
}

public static class ValueList
{
    [NotNull]
    public static ValueList<T> ToValueList<T>([NotNull] this IEnumerable<T> source)
    {
        return new ValueList<T>(source.ToImmutableList());
    }

    [NotNull]
    public static ValueList<T> ToValueList<T>([NotNull] this ImmutableList<T> source)
    {
        return new ValueList<T>(source);
    }

    [NotNull]
    public static ValueList<T> Create<T>(T item)
    {
        return ImmutableList.Create(item).ToValueList();
    }

    [NotNull]
    public static ValueList<T> Create<T>(params T[] items)
    {
        return ImmutableList.Create(items).ToValueList();
    }

    [NotNull]
    public static ValueList<T> Concat<T>(params ValueList<T>[] lists)
    {
        return lists.SelectMany(list => list).ToValueList();
    }
}
