using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.PositionEntity;
using RineaR.MadeHighlow.ActionFragments.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record PositionFailedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] PositionEntityResult FailedResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
