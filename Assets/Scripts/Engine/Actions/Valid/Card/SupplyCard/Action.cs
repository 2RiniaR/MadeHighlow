using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record Action([NotNull] PlayerID TargetID, [NotNull] Card InitialStatus) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.SupplyCard(history, this);
        }
    }
}
