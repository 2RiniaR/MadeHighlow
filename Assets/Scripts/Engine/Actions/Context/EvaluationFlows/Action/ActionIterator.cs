using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EvaluationFlows.Action
{
    public class ActionIterator : IActionIterator
    {
        public ActionIterator(IEvaluationContext context)
        {
            Context = context;
        }

        [NotNull] private IEvaluationContext Context { get; }

        public ValueList<Event<ReactedResult<IValidResult>>> Iterate(
            ref IHistory history,
            ValueList<IValidAction> actions
        )
        {
            var events = ValueList<Event<ReactedResult<IValidResult>>>.Empty;

            foreach (var action in actions)
            {
                var result = Context.Actions.Run(history, action);
                history = history.Appended(result, out var @event);
                events = events.Add(@event);
            }

            return events;
        }
    }
}
