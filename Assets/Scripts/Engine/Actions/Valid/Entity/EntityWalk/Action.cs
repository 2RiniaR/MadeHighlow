using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record Action([NotNull] EntityID ActorID, [NotNull] Route Route, [NotNull] Cost Available) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.EntityWalk(history, this);
        }
    }
}
