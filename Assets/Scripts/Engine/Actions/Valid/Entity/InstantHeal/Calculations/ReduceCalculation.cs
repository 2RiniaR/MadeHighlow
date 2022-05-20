using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public record ReduceCalculation([NotNull] HealReduction HealReduction) : InstantHealCalculation;
}
