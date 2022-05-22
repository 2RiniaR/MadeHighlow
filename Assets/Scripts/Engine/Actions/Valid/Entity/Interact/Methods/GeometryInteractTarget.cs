using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record GeometryInteractTarget(
        [NotNull] Position3D Position3D,
        [NotNull] [ItemNotNull] ValueList<ValidAction> Effects
    );
}
