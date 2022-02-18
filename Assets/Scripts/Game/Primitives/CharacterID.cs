using System;

namespace Game.Primitives
{
    public readonly struct CharacterID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private CharacterID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(CharacterID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static CharacterID None => new CharacterID(NoneInternalValue);
        public static CharacterID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new CharacterID(identity);
        }
    }
}
