using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public interface IInstantHealCalculator : IPriority<IInstantHealCalculator>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<InstantHealCalculation>> InstantHealCalculations(
            [NotNull] IHistory history,
            [NotNull] InstantHealAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealCalculation>> collected
        );
    }
}
