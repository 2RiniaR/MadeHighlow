using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record RejectedResult(
        [NotNull] DestroyEntityAction Action,
        [NotNull] DestroyEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
