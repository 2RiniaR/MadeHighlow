using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public record SucceedResult(
        [NotNull] CreateComponentAction Action,
        [NotNull] CreateComponentProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CreateComponentRejection>> RejectionInterrupts
    ) : CreateComponentResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
