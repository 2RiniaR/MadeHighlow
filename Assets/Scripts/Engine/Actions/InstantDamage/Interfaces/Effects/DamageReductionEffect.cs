using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DamageReductionEffect([NotNull] DamageReduction DamageReduction) : InstantDamageEffect;
}
