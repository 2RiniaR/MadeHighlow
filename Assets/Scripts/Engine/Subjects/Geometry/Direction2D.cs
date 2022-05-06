using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     フィールド上での方向
    /// </summary>
    public sealed record Direction2D
    {
        private Type InternalValue { get; init; }

        [NotNull] public static Direction2D XPositive => new() { InternalValue = Type.XPositive };

        [NotNull] public static Direction2D XNegative => new() { InternalValue = Type.XNegative };

        [NotNull] public static Direction2D YPositive => new() { InternalValue = Type.YPositive };
        [NotNull] public static Direction2D YNegative => new() { InternalValue = Type.YNegative };

        [NotNull]
        public Direction2D Backward => InternalValue switch
        {
            Type.XPositive => XNegative,
            Type.XNegative => XPositive,
            Type.YPositive => YNegative,
            Type.YNegative => YPositive,
            _ => throw new ArgumentOutOfRangeException(),
        };

        [NotNull]
        public Direction2D RightSide => InternalValue switch
        {
            Type.XPositive => YNegative,
            Type.XNegative => YPositive,
            Type.YPositive => XPositive,
            Type.YNegative => XNegative,
            _ => throw new ArgumentOutOfRangeException(),
        };

        [NotNull] public Direction2D LeftSide => Backward.RightSide;

        [NotNull]
        public static Direction2D FromVector([NotNull] in Vector2D vector)
        {
            if (vector == Vector2D.XPositive)
            {
                return XPositive;
            }

            if (vector == Vector2D.XNegative)
            {
                return XNegative;
            }

            if (vector == Vector2D.YPositive)
            {
                return YPositive;
            }

            if (vector == Vector2D.YNegative)
            {
                return YNegative;
            }

            throw new ArgumentException("The vector could not be convert to direction.");
        }

        [NotNull]
        public Vector2D ToVector()
        {
            return InternalValue switch
            {
                Type.XPositive => Vector2D.XPositive,
                Type.XNegative => Vector2D.XNegative,
                Type.YPositive => Vector2D.YPositive,
                Type.YNegative => Vector2D.YNegative,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private enum Type
        {
            XPositive,
            XNegative,
            YPositive,
            YNegative,
        }
    }
}