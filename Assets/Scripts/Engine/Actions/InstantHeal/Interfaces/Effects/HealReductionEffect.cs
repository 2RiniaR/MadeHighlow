using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record HealReductionEffect([NotNull] HealReduction HealReduction) : InstantHealEffect;
}
