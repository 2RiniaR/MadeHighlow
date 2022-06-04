using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record Action(ID SourceID, [NotNull] EntityID TargetID, [NotNull] KnockBack KnockBack) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.KnockBack(history, this);
        }
    }
}
