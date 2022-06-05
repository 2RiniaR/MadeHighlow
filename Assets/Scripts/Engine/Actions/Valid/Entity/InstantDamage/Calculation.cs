using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record Calculation([NotNull] ComponentID Effector)
    {
        public DamageReduction Reduction { get; init; }
    }
}
