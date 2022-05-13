using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record DestroyedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
        [NotNull] PositionEntity.SucceedResult PositionEntityResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
