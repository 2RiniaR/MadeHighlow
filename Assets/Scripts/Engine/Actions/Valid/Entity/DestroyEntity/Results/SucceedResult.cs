using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record SucceedResult(
        [NotNull] Entity Destroyed,
        [NotNull] ValueList<ReactedResult<RemoveComponent.SucceedResult>> RemoveComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityEffect>> Interrupts
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            world = RemoveComponentResults.Aggregate(world, (current, result) => result.Simulate(current));
            world = Destroyed.DeleteFrom(world);
            return world;
        }
    }
}
