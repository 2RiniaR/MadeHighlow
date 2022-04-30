using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record SucceedBigBangEvent() : BigBangEvent(BigBangResult.Succeed)
    {
        [NotNull] public World GeneratedWorld { get; init; } = new();

        public override World Simulate(in World world)
        {
            return GeneratedWorld;
        }
    }
}