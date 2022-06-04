using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record ReduceCalculation([NotNull] HealReduction HealReduction) : Calculation;
}
