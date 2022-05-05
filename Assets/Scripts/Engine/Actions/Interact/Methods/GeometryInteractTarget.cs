using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Methods
{
    public record GeometryInteractTarget
    {
        [NotNull] public Position3D Position { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IValidatable> Effects { get; init; } = ValueObjectList<IValidatable>.Empty;
    }
}