using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public interface IInstantDamageCalculator : IPriority<IInstantDamageCalculator>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<InstantDamageCalculation>> InstantDamageCalculations(
            [NotNull] IHistory history,
            [NotNull] InstantDamageAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageCalculation>> collected
        );
    }
}
