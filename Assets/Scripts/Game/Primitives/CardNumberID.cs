using System;

namespace Game.Primitives
{
    public readonly struct CardNumberID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private CardNumberID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(CardNumberID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static CardNumberID None => new CardNumberID(NoneInternalValue);
        public static CardNumberID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new CardNumberID(identity);
        }
    }
}
