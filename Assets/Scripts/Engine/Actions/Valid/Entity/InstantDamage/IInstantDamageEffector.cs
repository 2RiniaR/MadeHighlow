using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public interface IInstantDamageEffector : IPriority<IInstantDamageEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<InstantDamageEffect>> EffectsOnInstantDamage(
            [NotNull] IHistory history,
            [NotNull] InstantDamageAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageEffect>> collected
        );
    }
}
