using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record SucceedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults,
        [NotNull] Fragment.PositionEntity.SucceedResult PositionEntityResult,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityEffect>> Interrupts,
        [NotNull] Entity Generated
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(RegisterEntityResult)
                .Then(AddComponentResults)
                .Then(PositionEntityResult)
                .Simulate(world);
        }
    }
}
