using System;

namespace RineaR.MadeHighlow
{
    public readonly struct ID : IComparable<ID>
    {
        public override int GetHashCode()
        {
            return (int)InternalValue;
        }

        public bool Equals(ID other)
        {
            return InternalValue != 0 && InternalValue == other.InternalValue;
        }

        public override bool Equals(object obj)
        {
            return obj is ID other && Equals(other);
        }

        private ID(uint internalValue)
        {
            InternalValue = internalValue;
        }

        public uint InternalValue { get; }

        /// <summary>
        ///     識別されない番号
        /// </summary>
        /// <remarks>`None`同士の比較はfalseとなる。</remarks>
        public static ID None => new(0);

        /// <summary>
        ///     内部値からIDを生成する
        /// </summary>
        /// <remarks>`0`は識別しない番号として確保されているため、入力できない。</remarks>
        /// <param name="value">内部値</param>
        /// <returns>生成したID</returns>
        /// <exception cref="ArgumentException">`0`が入力されたとき。</exception>
        public static ID From(uint value)
        {
            if (value == 0)
            {
                throw new ArgumentException();
            }

            return new ID(value);
        }

        public static bool operator ==(ID item1, ID item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(ID item1, ID item2)
        {
            return !(item1 == item2);
        }

        public int CompareTo(ID other)
        {
            return InternalValue.CompareTo(other.InternalValue);
        }
    }
}
