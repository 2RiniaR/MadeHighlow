using System;

namespace Game.Primitives
{
    public readonly struct ClientID
    {
        private const int NoneInternalValue = 0;
        private readonly int _internalValue;

        private ClientID(int internalValue)
        {
            _internalValue = internalValue;
        }

        public bool Equals(ClientID other)
        {
            return _internalValue == other._internalValue && _internalValue != NoneInternalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue;
        }

        public static ClientID None => new ClientID(NoneInternalValue);
        public static ClientID FromIdentity(int identity)
        {
            if (identity == NoneInternalValue) throw new ArgumentException("The identity must not be 0.");
            return new ClientID(identity);
        }
    }
}
