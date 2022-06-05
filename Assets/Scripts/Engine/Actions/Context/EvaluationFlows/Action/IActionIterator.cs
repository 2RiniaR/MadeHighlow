using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.Action
{
    public interface IActionIterator
    {
        [NotNull]
        ValueList<Event<ReactedResult<IValidResult>>> Iterate(
            [NotNull] ref IHistory history,
            [NotNull] [ItemNotNull] ValueList<IValidAction> actions
        );
    }
}
