using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record SucceedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] RegisterComponent.SucceedResult RegisterComponentResult,
        [NotNull] ValueList<Result> InitializeResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<AddComponentEffect>> Interrupts,
        [NotNull] Component Added
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            world = RegisterComponentResult.Simulate(world);
            world = InitializeResults.Aggregate(world, (current, result) => result.Simulate(current));
            return world;
        }
    }
}
