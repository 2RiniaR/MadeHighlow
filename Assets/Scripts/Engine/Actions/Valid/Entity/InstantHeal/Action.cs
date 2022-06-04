using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public record Action(ID SourceID, [NotNull] EntityID TargetID, [NotNull] Heal Heal) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.InstantHeal(history, this);
        }
    }
}
