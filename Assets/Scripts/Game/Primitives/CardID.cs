using System;

namespace Game.Primitives
{
    public readonly struct CardID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private CardID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(CardID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static CardID None => new CardID(NoneInternalValue);
        public static CardID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new CardID(identity);
        }
    }
}
