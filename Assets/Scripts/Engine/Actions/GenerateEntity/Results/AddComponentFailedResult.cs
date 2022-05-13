using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record AddComponentFailedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntity.SucceedResult RegisterEntityResult,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> SucceedResults,
        [NotNull] AddComponentResult FailedResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
