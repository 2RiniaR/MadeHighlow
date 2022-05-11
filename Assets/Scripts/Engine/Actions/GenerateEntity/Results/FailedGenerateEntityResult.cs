using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedGenerateEntityResult(
        [NotNull] Entity Entity,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [CanBeNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults = null,
        [CanBeNull] PositionEntityResult PositionEntityResult = null
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
