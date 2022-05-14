using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record SucceedStepResult(
        [NotNull] EntityID ActorEntityID,
        [NotNull] Direction2D Direction2D,
        [NotNull] [ItemNotNull] ValueList<StepOutReaction> StepOutReactions,
        [NotNull] [ItemNotNull] ValueList<StepInReaction> StepInReactions,
        [NotNull] [ItemNotNull] ValueList<Result> AfterActionResults,
        [NotNull] StepCost AvailableStepCost
    ) : StepResult
    {
        public override World Simulate(World world)
        {
            throw new NotImplementedException();
        }
    }
}
