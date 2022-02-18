using System;

namespace Game.Primitives
{
    public readonly struct MapID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private MapID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(MapID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static MapID None => new MapID(NoneInternalValue);
        public static MapID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new MapID(identity);
        }
    }
}
