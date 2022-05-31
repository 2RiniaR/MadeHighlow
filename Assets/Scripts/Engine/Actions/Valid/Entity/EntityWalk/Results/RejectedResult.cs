using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record RejectedResult(
        [NotNull] EntityWalkAction Action,
        [NotNull] EntityWalkProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityWalkRejection>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityWalkResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
