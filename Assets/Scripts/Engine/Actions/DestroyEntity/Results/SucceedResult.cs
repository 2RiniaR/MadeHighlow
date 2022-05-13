using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record SucceedResult(
        [NotNull] Entity Destroyed,
        [NotNull] ValueList<RemoveComponent.SucceedResult> RemoveComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityEffect>> Interrupts
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            world = RemoveComponentResults.Aggregate(world, (curr, result) => result.Simulate(curr));
            return Destroyed.DeleteFrom(world);
        }
    }
}
