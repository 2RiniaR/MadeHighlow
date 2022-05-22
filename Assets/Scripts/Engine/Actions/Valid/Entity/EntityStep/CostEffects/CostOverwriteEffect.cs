using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record CostOverwriteEffect([NotNull] EntityStepCost Value) : EntityStepCostEffect;
}
