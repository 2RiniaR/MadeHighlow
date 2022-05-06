using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GeometryInteractTarget(
        [NotNull] in Position3D Position3D,
        [NotNull] [ItemNotNull] in ValueObjectList<Action> Effects
    );
}