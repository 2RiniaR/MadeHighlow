using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public interface IInstantHealEffector
    {
        public ValueList<Interrupt<InstantHealEffect>> EffectsOnInstantHeal(
            [NotNull] IHistory context,
            ID sourceID,
            [NotNull] Entity target,
            [NotNull] Heal heal
        );
    }
}
