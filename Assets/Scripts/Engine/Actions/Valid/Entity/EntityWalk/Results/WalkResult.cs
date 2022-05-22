using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;

namespace RineaR.MadeHighlow.Actions
{
    public record WalkResult(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<EntityStepResult>> StepResults
    ) : ValidResult
    {
        public override World Simulate(World world)
        {
            return StepResults.Aggregate(world, (currentWorld, step) => step.Simulate(currentWorld));
        }
    }
}
