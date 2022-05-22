using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record SucceedResult(
        [NotNull] GenerateEntityAction Action,
        [NotNull] GenerateEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityRejection>> RejectionInterrupts
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
