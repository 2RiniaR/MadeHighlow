using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public interface ICalculator : IPriority<ICalculator>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<Calculation>> InstantDamageCalculations(
            [NotNull] IHistory history,
            [NotNull] Action action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<Calculation>> collected
        );
    }
}
