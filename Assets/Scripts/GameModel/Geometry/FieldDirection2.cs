using System;

namespace RineaR.MadeHighlow.GameModel.Geometry
{
    [Serializable]
    public struct FieldDirection2
    {
        public enum Type
        {
            HorizontalPositive,
            HorizontalNegative,
            VerticalPositive,
            VerticalNegative,
        }

        public Type value;

        public static FieldDirection2 HorizontalPositive => new() { value = Type.HorizontalPositive };
        public static FieldDirection2 HorizontalNegative => new() { value = Type.HorizontalNegative };
        public static FieldDirection2 VerticalPositive => new() { value = Type.VerticalPositive };
        public static FieldDirection2 VerticalNegative => new() { value = Type.VerticalNegative };

        public FieldDirection2 Backward => value switch
        {
            Type.HorizontalPositive => HorizontalNegative,
            Type.HorizontalNegative => HorizontalPositive,
            Type.VerticalPositive => VerticalNegative,
            Type.VerticalNegative => VerticalPositive,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public FieldDirection2 RightSide => value switch
        {
            Type.HorizontalPositive => VerticalNegative,
            Type.HorizontalNegative => VerticalPositive,
            Type.VerticalPositive => HorizontalPositive,
            Type.VerticalNegative => HorizontalNegative,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public FieldDirection2 LeftSide => Backward.RightSide;

        public FieldDirection3 To3D()
        {
            var vector = ToVector();
            return FieldDirection3.FromVector(new FieldVector3
                { horizontal = vector.horizontal, vertical = vector.vertical, height = 0 });
        }

        public static FieldDirection2 FromVector(FieldVector2 vector)
        {
            if (vector == FieldVector2.XPositive)
            {
                return HorizontalPositive;
            }

            if (vector == FieldVector2.XNegative)
            {
                return HorizontalNegative;
            }

            if (vector == FieldVector2.YPositive)
            {
                return VerticalPositive;
            }

            if (vector == FieldVector2.YNegative)
            {
                return VerticalNegative;
            }

            throw new ArgumentException("The vector could not be convert to direction.");
        }

        public FieldVector2 ToVector()
        {
            return value switch
            {
                Type.HorizontalPositive => FieldVector2.XPositive,
                Type.HorizontalNegative => FieldVector2.XNegative,
                Type.VerticalPositive => FieldVector2.YPositive,
                Type.VerticalNegative => FieldVector2.YNegative,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(FieldDirection2 other)
        {
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return (int)value;
        }

        public static bool operator ==(FieldDirection2 l, FieldDirection2 r)
        {
            return l.Equals(r);
        }

        public static bool operator !=(FieldDirection2 l, FieldDirection2 r)
        {
            return !(l == r);
        }
    }
}