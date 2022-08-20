using System;

namespace RineaR.MadeHighlow.GameModel
{
    [Serializable]
    public class FigureMedo : IComparable<FigureMedo>
    {
        public int value;

        public int CompareTo(FigureMedo other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return value.CompareTo(other.value);
        }
    }
}