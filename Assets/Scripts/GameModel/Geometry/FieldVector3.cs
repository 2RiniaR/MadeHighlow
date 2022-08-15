using System;

namespace RineaR.MadeHighlow.GameModel.Geometry
{
    [Serializable]
    public struct FieldVector3
    {
        public int horizontal;
        public int vertical;
        public int height;

        public static FieldVector3 Zero => new() { horizontal = 0, vertical = 0, height = 0 };
        public static FieldVector3 XPositive => new() { horizontal = 1, vertical = 0, height = 0 };
        public static FieldVector3 XNegative => new() { horizontal = -1, vertical = 0, height = 0 };
        public static FieldVector3 YPositive => new() { horizontal = 0, vertical = 1, height = 0 };
        public static FieldVector3 YNegative => new() { horizontal = 0, vertical = -1, height = 0 };
        public static FieldVector3 ZPositive => new() { horizontal = 0, vertical = 0, height = 1 };
        public static FieldVector3 ZNegative => new() { horizontal = 0, vertical = 0, height = -1 };

        public static FieldVector3 operator +(FieldVector3 l, FieldVector3 r)
        {
            return new FieldVector3
            {
                horizontal = l.horizontal + r.horizontal, vertical = l.vertical + r.vertical,
                height = l.height + r.height,
            };
        }

        public static FieldVector3 operator -(FieldVector3 l, FieldVector3 r)
        {
            return l + -r;
        }

        public static FieldVector3 operator -(FieldVector3 l)
        {
            return new FieldVector3 { horizontal = -l.horizontal, vertical = -l.vertical, height = -l.height };
        }

        public static FieldVector3 operator *(FieldVector3 l, int r)
        {
            return new FieldVector3
                { horizontal = l.horizontal * r, vertical = l.vertical * r, height = l.height * r };
        }

        public FieldVector3 ExtendTo(FieldDirection3 direction3, int distance)
        {
            var vector = direction3.ToVector() * distance;
            return this + vector;
        }

        public FieldVector2 To2D()
        {
            return new FieldVector2 { horizontal = horizontal, vertical = vertical };
        }

        public override string ToString()
        {
            return $"({horizontal}, {vertical}, {height})";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(FieldVector3 other)
        {
            return horizontal == other.horizontal && vertical == other.vertical && height == other.height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(horizontal, vertical);
        }

        public static bool operator ==(FieldVector3 l, FieldVector3 r)
        {
            return l.Equals(r);
        }

        public static bool operator !=(FieldVector3 l, FieldVector3 r)
        {
            return !(l == r);
        }
    }
}