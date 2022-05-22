using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record SucceedResult
        ([NotNull] DeleteCardAction Action, [NotNull] DeleteCardProcess Process) : DeleteCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
