using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public interface IInstantHealEffector
    {
        public ValueList<Interrupt<InstantHealEffect>> EffectsOnInstantHeal(
            [NotNull] IActionContext context,
            ID sourceID,
            [NotNull] Entity target,
            [NotNull] Heal heal
        );
    }
}
