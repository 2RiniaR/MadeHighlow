using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record DirectInteractTarget(
        [NotNull] EntityID TargetEntityID,
        [NotNull] [ItemNotNull] ValueList<ValidAction> Effects
    );
}
