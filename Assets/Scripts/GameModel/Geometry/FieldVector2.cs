using System;

namespace RineaR.MadeHighlow.GameModel.Geometry
{
    [Serializable]
    public struct FieldVector2
    {
        public int horizontal;
        public int vertical;

        public static FieldVector2 Zero => new() { horizontal = 0, vertical = 0 };
        public static FieldVector2 XPositive => new() { horizontal = 1, vertical = 0 };
        public static FieldVector2 XNegative => new() { horizontal = -1, vertical = 0 };
        public static FieldVector2 YPositive => new() { horizontal = 0, vertical = 1 };
        public static FieldVector2 YNegative => new() { horizontal = 0, vertical = -1 };

        public static FieldVector2 operator +(FieldVector2 l, FieldVector2 r)
        {
            return new FieldVector2 { horizontal = l.horizontal + r.horizontal, vertical = l.vertical + r.vertical };
        }

        public static FieldVector2 operator -(FieldVector2 l, FieldVector2 r)
        {
            return l + -r;
        }

        public static FieldVector2 operator -(FieldVector2 l)
        {
            return new FieldVector2 { horizontal = -l.horizontal, vertical = -l.vertical };
        }

        public static FieldVector2 operator *(FieldVector2 l, int r)
        {
            return new FieldVector2 { horizontal = l.horizontal * r, vertical = l.vertical * r };
        }

        public FieldVector2 ExtendTo(FieldDirection2 direction2, int distance)
        {
            var vector = direction2.ToVector() * distance;
            return this + vector;
        }

        public FieldVector3 To3D(int height)
        {
            return new FieldVector3 { horizontal = horizontal, vertical = vertical, height = height };
        }

        public override string ToString()
        {
            return $"({horizontal}, {vertical})";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(FieldVector2 other)
        {
            return horizontal == other.horizontal && vertical == other.vertical;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(horizontal, vertical);
        }

        public static bool operator ==(FieldVector2 l, FieldVector2 r)
        {
            return l.Equals(r);
        }

        public static bool operator !=(FieldVector2 l, FieldVector2 r)
        {
            return !(l == r);
        }
    }
}