using System;

namespace Game.Primitives
{
    public readonly struct CardTypeID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private CardTypeID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(CardTypeID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static CardTypeID None => new CardTypeID(NoneInternalValue);
        public static CardTypeID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new CardTypeID(identity);
        }
    }
}
