using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GeometryInteractTarget
    {
        [NotNull] public Position3D Position { get; init; } = new();

        [NotNull] [ItemNotNull] public ValueObjectList<Action> Effects { get; init; } = ValueObjectList<Action>.Empty;
    }
}