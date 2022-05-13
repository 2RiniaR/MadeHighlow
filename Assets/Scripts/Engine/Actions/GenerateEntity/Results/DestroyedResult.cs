using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record DestroyedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntity.SucceedResult RegisterEntityResult,
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
