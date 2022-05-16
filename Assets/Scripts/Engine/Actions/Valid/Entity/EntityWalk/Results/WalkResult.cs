using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
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
