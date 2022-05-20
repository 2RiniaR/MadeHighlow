using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public record RejectedResult(
        [NotNull] EntityFlyAction Action,
        [NotNull] EntityFlyProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityFlyResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
