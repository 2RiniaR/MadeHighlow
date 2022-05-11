using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record RejectedGenerateEntityResult
        ([NotNull] Entity Entity, [NotNull] ComponentID RejectedComponentID) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
