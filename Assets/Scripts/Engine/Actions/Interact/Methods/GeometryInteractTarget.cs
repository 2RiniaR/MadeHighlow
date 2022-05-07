using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GeometryInteractTarget(
        [NotNull] Position3D Position3D,
        [NotNull] [ItemNotNull] ValueObjectList<Action> Effects
    );
}