using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractTarget(
        [NotNull] EntityID TargetEntityID,
        [NotNull] [ItemNotNull] ValueObjectList<Action> Effects
    );
}