using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractTarget(
        [NotNull] EntityID TargetEntityID,
        [NotNull] [ItemNotNull] ValueList<Action> Effects
    );
}
