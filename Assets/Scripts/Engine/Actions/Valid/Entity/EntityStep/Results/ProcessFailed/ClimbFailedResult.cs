using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record ClimbFailedResult(
        [NotNull] EntityStepAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> SucceedResults,
        [NotNull] MoveEntityResult Failed
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
