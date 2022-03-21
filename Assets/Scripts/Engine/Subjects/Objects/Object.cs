using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects
{
    /// <summary>
    ///     「フィールド」上にあるオブジェクト
    /// </summary>
    public abstract record Object(in ObjectType ObjectType)
    {
        public ObjectType ObjectType { get; } = ObjectType;

        public ID<Object> ID { get; init; } = ID<Object>.None;
        [NotNull] public ImmutableList<Component> Components { get; init; } = ImmutableList<Component>.Empty;
        [NotNull] public Position2D Position2D { get; init; } = Position2D.Zero;
        [NotNull] public Direction2D Direction2D { get; init; } = Direction2D.XNegative;
    }
}