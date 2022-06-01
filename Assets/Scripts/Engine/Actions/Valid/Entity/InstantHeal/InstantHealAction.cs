using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record InstantHealAction(ID SourceID, [NotNull] EntityID TargetID, [NotNull] Heal Heal) : IValidAction;
}
