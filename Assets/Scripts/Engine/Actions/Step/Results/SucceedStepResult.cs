using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedStepResult(
        [NotNull] EntityID ActorEntityID,
        [NotNull] Direction2D Direction2D,
        [NotNull] [ItemNotNull] ValueObjectList<StepOutReaction> StepOutReactions,
        [NotNull] [ItemNotNull] ValueObjectList<StepInReaction> StepInReactions,
        [NotNull] [ItemNotNull] ValueObjectList<Result> AfterActionResults,
        [NotNull] StepCost AvailableStepCost
    ) : StepResult
    {
        public override World Simulate(World world)
        {
            throw new NotImplementedException();
        }
    }
}