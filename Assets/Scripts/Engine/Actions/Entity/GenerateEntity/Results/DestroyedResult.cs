using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record DestroyedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] ActionFragments.PositionEntity.SucceedResult PositionEntityResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
