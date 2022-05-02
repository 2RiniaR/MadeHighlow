using System;

namespace RineaR.MadeHighlow
{
    public readonly struct ID<T> : IComparable<ID<T>>
    {
        public override int GetHashCode()
        {
            return (int)InternalValue;
        }

        public bool Equals(ID<T> other)
        {
            return InternalValue != 0 && InternalValue == other.InternalValue;
        }

        public override bool Equals(object obj)
        {
            return obj is ID<T> other && Equals(other);
        }

        private ID(uint internalValue)
        {
            InternalValue = internalValue;
        }

        private uint InternalValue { get; }

        public static ID<T> None => new(0);

        public static ID<T> From(uint value)
        {
            if (value == 0) throw new ArgumentException();
            return new ID<T>(value);
        }

        public static bool operator ==(ID<T> item1, ID<T> item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(ID<T> item1, ID<T> item2)
        {
            return !(item1 == item2);
        }

        public int CompareTo(ID<T> other)
        {
            return InternalValue.CompareTo(other.InternalValue);
        }
    }
}