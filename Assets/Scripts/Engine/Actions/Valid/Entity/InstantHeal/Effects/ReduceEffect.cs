using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public record ReduceEffect([NotNull] HealReduction HealReduction) : InstantHealEffect;
}
