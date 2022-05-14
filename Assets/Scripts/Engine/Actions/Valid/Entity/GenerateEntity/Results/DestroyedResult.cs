using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record DestroyedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] Fragment.PositionEntity.SucceedResult PositionEntityResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
