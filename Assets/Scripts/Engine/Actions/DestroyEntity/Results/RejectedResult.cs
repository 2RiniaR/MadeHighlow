using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record RejectedResult(
        [NotNull] Entity Target,
        [NotNull] ComponentID RejectedID,
        [NotNull] ValueList<RemoveComponent.SucceedResult> RemoveComponents,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityEffect>> Interrupts
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
