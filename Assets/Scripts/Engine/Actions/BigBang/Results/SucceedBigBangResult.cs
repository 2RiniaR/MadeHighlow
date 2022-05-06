using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedBigBangResult([NotNull] in World GeneratedWorld) : BigBangResult
    {
        public override World Simulate(in World world)
        {
            return GeneratedWorld;
        }
    }
}