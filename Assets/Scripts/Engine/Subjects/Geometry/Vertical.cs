using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public sealed record Vertical(in int Value = 0)
    {
        [NotNull]
        public static Vertical operator +([NotNull] in Vertical l, in int r)
        {
            return new Vertical(l.Value + r);
        }

        [NotNull]
        public static Vertical operator -([NotNull] in Vertical l, in int r)
        {
            return new Vertical(l.Value - r);
        }
    }
}