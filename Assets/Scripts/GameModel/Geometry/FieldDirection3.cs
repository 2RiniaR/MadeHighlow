using System;

namespace RineaR.MadeHighlow.GameModel.Geometry
{
    [Serializable]
    public struct FieldDirection3
    {
        public enum Type
        {
            HorizontalPositive,
            HorizontalNegative,
            VerticalPositive,
            VerticalNegative,
            HeightPositive,
            HeightNegative,
        }

        public Type value;

        public static FieldDirection3 HorizontalPositive => new() { value = Type.HorizontalPositive };
        public static FieldDirection3 HorizontalNegative => new() { value = Type.HorizontalNegative };
        public static FieldDirection3 VerticalPositive => new() { value = Type.VerticalPositive };
        public static FieldDirection3 VerticalNegative => new() { value = Type.VerticalNegative };
        public static FieldDirection3 HeightPositive => new() { value = Type.HeightPositive };
        public static FieldDirection3 HeightNegative => new() { value = Type.HeightNegative };

        public FieldDirection3 Backward => value switch
        {
            Type.HorizontalPositive => HorizontalNegative,
            Type.HorizontalNegative => HorizontalPositive,
            Type.VerticalPositive => VerticalNegative,
            Type.VerticalNegative => VerticalPositive,
            Type.HeightPositive => HeightPositive,
            Type.HeightNegative => HeightNegative,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public FieldDirection3 Inverse => value switch
        {
            Type.HorizontalPositive => HorizontalNegative,
            Type.HorizontalNegative => HorizontalPositive,
            Type.VerticalPositive => VerticalNegative,
            Type.VerticalNegative => VerticalPositive,
            Type.HeightPositive => HeightNegative,
            Type.HeightNegative => HeightPositive,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public FieldDirection3 RightSide => value switch
        {
            Type.HorizontalPositive => VerticalNegative,
            Type.HorizontalNegative => VerticalPositive,
            Type.VerticalPositive => HorizontalPositive,
            Type.VerticalNegative => HorizontalNegative,
            Type.HeightPositive => HeightPositive,
            Type.HeightNegative => HeightNegative,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public FieldDirection3 LeftSide => Backward.RightSide;

        public static FieldDirection3 FromVector(FieldVector3 vector)
        {
            if (vector == FieldVector3.XPositive)
            {
                return HorizontalPositive;
            }

            if (vector == FieldVector3.XNegative)
            {
                return HorizontalNegative;
            }

            if (vector == FieldVector3.YPositive)
            {
                return VerticalPositive;
            }

            if (vector == FieldVector3.YNegative)
            {
                return VerticalNegative;
            }

            if (vector == FieldVector3.ZPositive)
            {
                return HeightPositive;
            }

            if (vector == FieldVector3.ZNegative)
            {
                return HeightNegative;
            }

            throw new ArgumentException("The vector could not be convert to direction.");
        }

        public FieldVector3 ToVector()
        {
            return value switch
            {
                Type.HorizontalPositive => FieldVector3.XPositive,
                Type.HorizontalNegative => FieldVector3.XNegative,
                Type.VerticalPositive => FieldVector3.YPositive,
                Type.VerticalNegative => FieldVector3.YNegative,
                Type.HeightPositive => FieldVector3.ZPositive,
                Type.HeightNegative => FieldVector3.ZNegative,
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

        public bool Equals(FieldDirection3 other)
        {
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return (int)value;
        }

        public static bool operator ==(FieldDirection3 l, FieldDirection3 r)
        {
            return l.Equals(r);
        }

        public static bool operator !=(FieldDirection3 l, FieldDirection3 r)
        {
            return !(l == r);
        }
    }
}