using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public interface ICalculator : IPriority<ICalculator>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<Calculation>> Calculations([NotNull] CalculationContext context);
    }
}
