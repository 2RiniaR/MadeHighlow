using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record Action(ID SourceID, [NotNull] EntityID TargetID, [NotNull] Damage Damage) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.InstantDamage(history, this);
        }
    }
}
