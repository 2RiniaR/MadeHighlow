using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record EntityWalkAction(
        [NotNull] EntityID ActorID,
        [NotNull] EntityWalkRoute Route,
        [NotNull] EntityStepCost Available
    ) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.EntityWalk(history, this);
        }
    }
}
