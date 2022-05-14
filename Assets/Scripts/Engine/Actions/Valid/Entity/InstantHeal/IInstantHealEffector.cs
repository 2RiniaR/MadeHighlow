using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public interface IInstantHealEffector
    {
        public ValueList<Interrupt<InstantHealEffect>> EffectsOnInstantHeal(
            [NotNull] IHistory history,
            ID sourceID,
            [NotNull] Entity target,
            [NotNull] Heal heal
        );
    }
}
