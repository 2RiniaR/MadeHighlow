using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record Action([NotNull] EntityID TargetID, [NotNull] Route Route) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.EntityFly(history, this);
        }
    }
}
