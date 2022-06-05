using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public interface ICalculator : IPriority<ICalculator>
    {
        [CanBeNull]
        public ValueList<Interrupt<Calculation>> Calculations([NotNull] CalculationContext context);
    }
}
