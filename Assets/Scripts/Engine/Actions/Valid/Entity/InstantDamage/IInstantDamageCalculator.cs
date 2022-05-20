using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public interface IInstantDamageCalculator : IPriority<IInstantDamageCalculator>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<InstantDamageCalculation>> DamageCalculations(
            [NotNull] IHistory history,
            [NotNull] InstantDamageAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageCalculation>> collected
        );
    }
}
