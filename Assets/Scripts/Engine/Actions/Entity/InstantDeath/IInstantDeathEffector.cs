using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public interface IInstantDeathEffector
    {
        public ValueList<Interrupt<InstantDeathEffect>> EffectsOnInstantDeath(
            [NotNull] IHistory history,
            ID sourceID,
            [NotNull] Entity target
        );
    }
}
