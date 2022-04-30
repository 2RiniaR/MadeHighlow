using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public sealed record Horizontal(in int Value = 0)
    {
        [NotNull]
        public static Horizontal operator +([NotNull] in Horizontal l, in int r)
        {
            return new Horizontal(l.Value + r);
        }

        [NotNull]
        public static Horizontal operator -([NotNull] in Horizontal l, in int r)
        {
            return new Horizontal(l.Value - r);
        }
    }
}