using System;

namespace Game.Primitives
{
    public readonly struct UnitID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private UnitID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(UnitID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static UnitID None => new UnitID(NoneInternalValue);
        public static UnitID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new UnitID(identity);
        }
    }
}
