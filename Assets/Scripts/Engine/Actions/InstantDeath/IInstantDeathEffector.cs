using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public interface IInstantDeathEffector
    {
        public ValueList<Interrupt<InstantDeathEffect>> EffectsOnInstantDeath(
            [NotNull] IActionContext context,
            ID sourceID,
            [NotNull] Entity target
        );
    }
}
