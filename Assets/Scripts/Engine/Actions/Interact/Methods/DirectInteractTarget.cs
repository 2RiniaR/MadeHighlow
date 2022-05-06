using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractTarget(
        [NotNull] in EntityID TargetEntityID,
        [NotNull] [ItemNotNull] in ValueObjectList<Action> Effects
    );
}