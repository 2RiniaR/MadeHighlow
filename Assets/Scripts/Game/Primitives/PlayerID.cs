using System;

namespace Game.Primitives
{
    public readonly struct PlayerID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private PlayerID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(PlayerID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static PlayerID None => new PlayerID(NoneInternalValue);
        public static PlayerID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new PlayerID(identity);
        }
    }
}
