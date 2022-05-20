using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public record RejectedResult(
        [NotNull] DeleteComponentAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DeleteComponentRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : DeleteComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
