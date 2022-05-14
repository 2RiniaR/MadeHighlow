using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record ReduceEffect([NotNull] HealReduction HealReduction) : InstantHealEffect;
}
