using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record RejectedResult(
        [NotNull] GenerateEntityAction Action,
        [NotNull] GenerateEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
