using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IPriority<T> : IComparable<T> where T : IPriority<T>
    {
        [NotNull] public Priority Priority { get; }

        int IComparable<T>.CompareTo(T other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Priority.CompareTo(other.Priority);
        }
    }
}
