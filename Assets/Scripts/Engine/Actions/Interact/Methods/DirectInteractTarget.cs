using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record DirectInteractTarget(
        [NotNull] EntityID TargetEntityID,
        [NotNull] [ItemNotNull] ValueList<Action> Effects
    );
}
