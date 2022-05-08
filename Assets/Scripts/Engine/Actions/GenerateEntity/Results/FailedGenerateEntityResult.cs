using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedGenerateEntityResult(
        [NotNull] Entity Entity,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
