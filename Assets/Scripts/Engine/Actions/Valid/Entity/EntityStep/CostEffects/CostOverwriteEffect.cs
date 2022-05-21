using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record CostOverwriteEffect([NotNull] EntityStepCost Value) : EntityStepCostEffect;
}
