using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedBigBangResult() : BigBangResult(BigBangResultCode.Succeed)
    {
        [NotNull] public World GeneratedWorld { get; init; } = new();

        public override World Simulate(in World world)
        {
            return GeneratedWorld;
        }
    }
}