using System;

namespace Game.Primitives
{
    public readonly struct GameID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private GameID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(GameID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static GameID None => new GameID(NoneInternalValue);
        public static GameID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new GameID(identity);
        }
    }
}
