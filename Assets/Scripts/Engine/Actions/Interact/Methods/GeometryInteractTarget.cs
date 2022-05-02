using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Methods
{
    public record GeometryInteractTarget
    {
        [NotNull] public Position3D Position { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<InteractionEffect> Effects { get; init; } = ValueObjectList<InteractionEffect>.Empty;
    }
}