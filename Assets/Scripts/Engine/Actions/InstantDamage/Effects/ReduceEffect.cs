using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record ReduceEffect([NotNull] DamageReduction DamageReduction) : InstantDamageEffect;
}
