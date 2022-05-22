using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record ShiftFailedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents,
        [NotNull] MoveEntityResult Failed
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
