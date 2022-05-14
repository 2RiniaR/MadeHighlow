using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public interface IInstantDamageEffector
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<InstantDamageEffect>> EffectsOnInstantDamage(
            [NotNull] IHistory history,
            ID sourceID,
            [NotNull] Entity target,
            [NotNull] Damage damage
        );
    }
}
