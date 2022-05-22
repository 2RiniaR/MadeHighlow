using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record ReduceCalculation([NotNull] DamageReduction DamageReduction) : InstantDamageCalculation;
}
