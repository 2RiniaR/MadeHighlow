using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record RejectedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntity.SucceedResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponentResults,
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
