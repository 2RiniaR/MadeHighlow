using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record RemoveComponentAction([NotNull] ComponentID TargetID) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.RemoveComponent(history, this);
        }
    }
}
