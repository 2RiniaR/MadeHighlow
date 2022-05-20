using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record ReduceCalculation([NotNull] DamageReduction DamageReduction) : InstantDamageCalculation;
}
