using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public interface ICalculator : IPriority<ICalculator>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<Calculation>> InstantHealCalculations(
            [NotNull] IHistory history,
            [NotNull] Action action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> collected
        );
    }
}
