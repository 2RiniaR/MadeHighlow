using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

public sealed class ValueObjectList<T> : IEnumerable<T>
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

    [NotNull] [ItemNotNull] private ImmutableList<T> Items { get; }

    [NotNull] [ItemNotNull] public static ValueObjectList<T> Empty => new();

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

    public ValueObjectList<T> ReplaceItem(Predicate<T> predicate, T newItem)
    {
        var index = Items.FindIndex(predicate);
        return Items.SetItem(index, newItem).ToValueObjectList();
    }

    [CanBeNull]
    public T Find(Predicate<T> predicate)
    {
        return Items.Find(predicate);
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<T> Where(Predicate<T> predicate)
    {
        return Items.FindAll(predicate).ToValueObjectList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<T> Add([NotNull] T item)
    {
        return Items.Add(item).ToValueObjectList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<T> AddRange([NotNull] [ItemNotNull] IEnumerable<T> items)
    {
        return Items.AddRange(items).ToValueObjectList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<TResult> SelectMany<TResult>(Func<T, IEnumerable<TResult>> selector)
    {
        return Items.SelectMany(selector).ToValueObjectList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<TResult> Cast<TResult>()
    {
        return Items.Cast<TResult>().ToValueObjectList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<TResult> WhereType<TResult>() where TResult : class
    {
        return Items.Select(item => item as TResult).Where(item => item != null).ToValueObjectList();
    }

    [NotNull]
    [ItemNotNull]
    public ValueObjectList<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        return Items.Select(selector).ToValueObjectList();
    }

    public TResult Aggregate<TResult>(TResult seed, Func<TResult, T, TResult> func)
    {
        return Items.Aggregate(seed, func);
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

    public static ValueObjectList<T> Concat<T>(params ValueObjectList<T>[] lists)
    {
        return lists.SelectMany(list => list).ToValueObjectList();
    }
}