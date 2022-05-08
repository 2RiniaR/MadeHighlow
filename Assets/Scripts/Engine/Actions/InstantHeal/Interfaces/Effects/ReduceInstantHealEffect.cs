using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ReduceInstantHealEffect([NotNull] HealReduction HealReduction) : InstantHealEffect;
}
