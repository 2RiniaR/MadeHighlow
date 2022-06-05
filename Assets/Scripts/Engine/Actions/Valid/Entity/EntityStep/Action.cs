using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record Action(
        [NotNull] EntityID ActorID,
        [NotNull] Direction2D Direction,
        [NotNull] Cost Available
    ) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.EntityStep(history, this);
        }
    }
}
