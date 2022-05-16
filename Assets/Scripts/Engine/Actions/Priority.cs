using System;

namespace RineaR.MadeHighlow.Actions
{
    public record Priority(uint Value) : IComparable<Priority>
    {
        public int CompareTo(Priority other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Value.CompareTo(other.Value);
        }
    }
}
