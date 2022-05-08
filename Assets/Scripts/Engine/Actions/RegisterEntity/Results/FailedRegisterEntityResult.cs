using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedRegisterEntityResult
        ([NotNull] Entity Entity, FailedRegisterEntityReason Reason) : RegisterEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
