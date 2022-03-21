using System;
using System.Collections.Immutable;

public static class ImmutableListExtension
{
    public static ImmutableList<T> ReplaceItem<T>(this ImmutableList<T> source, Predicate<T> predicate, T newItem)
    {
        var index = source.FindIndex(predicate);
        return source.SetItem(index, newItem);
    }
}