using System;

namespace Game.Primitives
{
    public readonly struct ItemID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private ItemID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(ItemID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static ItemID None => new ItemID(NoneInternalValue);
        public static ItemID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new ItemID(identity);
        }
    }
}
