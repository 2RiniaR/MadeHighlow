using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public record RejectedResult(
        [NotNull] MoveEntityAction Action,
        [NotNull] MoveEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<MoveEntityEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : MoveEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
