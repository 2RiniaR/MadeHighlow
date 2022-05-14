using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record RejectedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] PositionEntity.SucceedResult PositionEntityResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityEffect>> Interrupts,
        [NotNull] ComponentID RejectedComponentID
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
