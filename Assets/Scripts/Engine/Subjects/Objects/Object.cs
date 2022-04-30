using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「フィールド」上にあるオブジェクト
    /// </summary>
    public abstract record Object(in ObjectType ObjectType)
    {
        public ObjectType ObjectType { get; } = ObjectType;

        public ID<Object> ID { get; init; } = ID<Object>.None;
        [NotNull] public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;
        [NotNull] public Position2D Position2D { get; init; } = Position2D.Zero;
        [NotNull] public Direction2D Direction2D { get; init; } = Direction2D.XNegative;
    }
}