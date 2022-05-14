using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record ReduceEffect([NotNull] DamageReduction DamageReduction) : InstantDamageEffect;
}
