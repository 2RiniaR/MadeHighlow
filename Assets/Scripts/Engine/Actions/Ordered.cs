using System;

namespace RineaR.MadeHighlow
{
    public record Ordered<T>(uint Priority, T Value) : IComparable<Ordered<T>>
    {
        public int CompareTo(Ordered<T> other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Priority.CompareTo(other.Priority);
        }
    }
}
