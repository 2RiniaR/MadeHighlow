using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedBigBangResult([NotNull] World GeneratedWorld) : BigBangResult
    {
        public override World Simulate(World world)
        {
            return GeneratedWorld;
        }
    }
}
