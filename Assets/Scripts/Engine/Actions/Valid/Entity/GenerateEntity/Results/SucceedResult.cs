using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record SucceedResult(
        [NotNull] GenerateEntityAction Action,
        [NotNull] GenerateEntityProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityEffect>> Interrupts
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
