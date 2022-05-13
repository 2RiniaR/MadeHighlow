using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record PositionFailedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntity.SucceedResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionEntityResult FailedResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
