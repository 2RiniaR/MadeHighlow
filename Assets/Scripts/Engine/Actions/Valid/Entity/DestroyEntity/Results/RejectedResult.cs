using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record RejectedResult(
        [NotNull] DestroyEntityAction Action,
        [NotNull] DestroyEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
