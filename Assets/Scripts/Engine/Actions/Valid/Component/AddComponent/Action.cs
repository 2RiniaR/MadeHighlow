using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record Action([NotNull] IAttachableID TargetID, [NotNull] Component InitialStatus) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.AddComponent(history, this);
        }
    }
}
