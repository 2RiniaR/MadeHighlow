using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ReduceInstantDamageEffect([NotNull] DamageReduction DamageReduction) : InstantDamageEffect;
}
