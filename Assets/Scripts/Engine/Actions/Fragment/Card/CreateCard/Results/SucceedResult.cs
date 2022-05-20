using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateCard
{
    public record SucceedResult
        ([NotNull] CreateCardAction Action, [NotNull] CreateCardProcess Process) : CreateCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
