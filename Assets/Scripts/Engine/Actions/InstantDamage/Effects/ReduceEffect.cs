using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ReduceEffect([NotNull] DamageReduction DamageReduction) : InstantDamageEffect;
}
