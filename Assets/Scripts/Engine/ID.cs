using System;
using RineaR.MadeHighlow.Engine.Exceptions;

namespace RineaR.MadeHighlow.Engine
{
    public struct ID<T> : IComparable<ID<T>>
    {
        public bool Equals(ID<T> other)
        {
            return InternalValue != NoneInternalValue && InternalValue == other.InternalValue;
        }

        public override bool Equals(object obj)
        {
            return obj is ID<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return InternalValue;
        }

        public const int NoneInternalValue = -1;

        private ID(int internalValue)
        {
            InternalValue = internalValue;
        }

        public int InternalValue { get; }

        public static ID<T> None => new(NoneInternalValue);

        public static ID<T> From(int value)
        {
            if (value == NoneInternalValue) throw new InvalidIDException();
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