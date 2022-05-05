using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「フィールド」上での方向
    /// </summary>
    public sealed record Direction3D
    {
        [NotNull] public static Direction3D XPositive => new() { InternalValue = Type.XPositive };

        [NotNull] public static Direction3D XNegative => new() { InternalValue = Type.XNegative };

        [NotNull] public static Direction3D YPositive => new() { InternalValue = Type.YPositive };
        [NotNull] public static Direction3D YNegative => new() { InternalValue = Type.YNegative };
        [NotNull] public static Direction3D ZPositive => new() { InternalValue = Type.ZPositive };
        [NotNull] public static Direction3D ZNegative => new() { InternalValue = Type.ZNegative };

        [NotNull]
        public Direction3D Backward => InternalValue switch
        {
            Type.XPositive => XNegative,
            Type.XNegative => XPositive,
            Type.YPositive => YNegative,
            Type.YNegative => YPositive,
            Type.ZPositive => ZPositive,
            Type.ZNegative => ZNegative,
            _ => throw new ArgumentOutOfRangeException(),
        };

        [NotNull]
        public Direction3D Inverse => InternalValue switch
        {
            Type.XPositive => XNegative,
            Type.XNegative => XPositive,
            Type.YPositive => YNegative,
            Type.YNegative => YPositive,
            Type.ZPositive => ZNegative,
            Type.ZNegative => ZPositive,
            _ => throw new ArgumentOutOfRangeException(),
        };

        [NotNull]
        public Direction3D RightSide => InternalValue switch
        {
            Type.XPositive => YNegative,
            Type.XNegative => YPositive,
            Type.YPositive => XPositive,
            Type.YNegative => XNegative,
            Type.ZPositive => ZPositive,
            Type.ZNegative => ZNegative,
            _ => throw new ArgumentOutOfRangeException(),
        };

        [NotNull] public Direction3D LeftSide => Backward.RightSide;

        /// <summary>
        ///     方向
        /// </summary>
        private Type InternalValue { get; init; }

        [NotNull]
        public static Direction3D FromVector([NotNull] in Vector3D vector)
        {
            if (vector == Vector3D.XPositive)
            {
                return XPositive;
            }

            if (vector == Vector3D.XNegative)
            {
                return XNegative;
            }

            if (vector == Vector3D.YPositive)
            {
                return YPositive;
            }

            if (vector == Vector3D.YNegative)
            {
                return YNegative;
            }

            if (vector == Vector3D.ZPositive)
            {
                return ZPositive;
            }

            if (vector == Vector3D.ZNegative)
            {
                return ZNegative;
            }

            throw new ArgumentException("The vector could not be convert to direction.");
        }

        [NotNull]
        public Vector3D ToVector()
        {
            return InternalValue switch
            {
                Type.XPositive => Vector3D.XPositive,
                Type.XNegative => Vector3D.XNegative,
                Type.YPositive => Vector3D.YPositive,
                Type.YNegative => Vector3D.YNegative,
                Type.ZPositive => Vector3D.ZPositive,
                Type.ZNegative => Vector3D.ZNegative,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private enum Type
        {
            XPositive,
            XNegative,
            YPositive,
            YNegative,
            ZPositive,
            ZNegative,
        }
    }
}