using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

public sealed class ValuePriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
{
    public ValuePriorityQueue()
    {
        Items = ImmutableList<T>.Empty;
    }

    public ValuePriorityQueue(ImmutableList<T> items)
    {
        Items = items;
    }

    [NotNull] [ItemNotNull] private ImmutableList<T> Items { get; }

    [NotNull] [ItemNotNull] public static ValuePriorityQueue<T> Empty => new();

    public int Count => Items.Count;
    public bool IsEmpty => Count == 0;

    public T this[int index] => Items[index];

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    public override bool Equals(object obj)
    {
        return obj is ValuePriorityQueue<T> other ? Equals(other) : false;
    }

    private bool Equals(ValuePriorityQueue<T> other)
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

    [NotNull]
    [ItemNotNull]
    public ValuePriorityQueue<T> Enqueue([NotNull] T item)
    {
        return EnqueueRange(new[] { item });
    }

    [NotNull]
    [ItemNotNull]
    public ValuePriorityQueue<T> EnqueueRange([NotNull] [ItemNotNull] IEnumerable<T> appendItems)
    {
        var items = Items.ToBuilder();

        foreach (var item in appendItems)
        {
            items.Add(item);
            var child = items.Count - 1;
            while (child > 0)
            {
                var parent = (child - 1) / 2;
                if (items[child].CompareTo(items[parent]) >= 0) break;
                Swap(items, parent, child);
                child = parent;
            }
        }

        return new ValuePriorityQueue<T>(items.ToImmutable());
    }

    public (ValuePriorityQueue<T>, T) Dequeue()
    {
        var items = Items.ToBuilder();

        var last = items.Count - 1;
        var first = items[0];
        items[0] = items[last];
        items.RemoveAt(last--);
        var parent = 0;

        while (true)
        {
            var child = parent * 2 + 1;
            if (child > last) break;
            var rightChild = child + 1;
            if (rightChild <= last && items[rightChild].CompareTo(items[child]) < 0)
            {
                child = rightChild;
            }

            if (items[parent].CompareTo(items[child]) <= 0) break;
            Swap(items, parent, child);
            parent = child;
        }

        return (new ValuePriorityQueue<T>(items.ToImmutable()), first);
    }

    private static void Swap(ImmutableList<T>.Builder builder, int index1, int index2)
    {
        var tmp = builder[index1];
        builder[index1] = builder[index2];
        builder[index2] = tmp;
    }

    public static bool operator ==(ValuePriorityQueue<T> item1, ValuePriorityQueue<T> item2)
    {
        return !(item1 is null) && item1.Equals(item2);
    }

    public static bool operator !=(ValuePriorityQueue<T> item1, ValuePriorityQueue<T> item2)
    {
        return !(item1 == item2);
    }
}
