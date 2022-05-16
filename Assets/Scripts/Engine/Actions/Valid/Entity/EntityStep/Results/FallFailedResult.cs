using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public record FallFailedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] [ItemNotNull] ValueList<Fragment.MoveEntity.SucceedResult> ClimbResults,
        [NotNull] Fragment.MoveEntity.SucceedResult ShiftResult,
        [NotNull] [ItemNotNull] ValueList<Fragment.MoveEntity.SucceedResult> SucceedResults,
        [NotNull] MoveEntityResult FailedResult
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
