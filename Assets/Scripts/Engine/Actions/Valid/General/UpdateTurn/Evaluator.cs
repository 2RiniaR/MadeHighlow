using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            RunActions();
            IncrementTurn();
            return Result;
        }

        private void RunActions()
        {
            var interruptsQueue = ValuePriorityQueue<Interrupt<IValidAction>>.Empty;
            var actors = Context.Finder.GetAllComponents<IActor>(Simulating.World).Sort();
            foreach (var actor in actors)
            {
                var interrupts = actor.UpdateTurnActions(Simulating, Action, interruptsQueue.ToValueList());
                if (interrupts == null) continue;
                interruptsQueue = interruptsQueue.EnqueueRange(interrupts);
            }

            var actions = interruptsQueue.ToValueList();

            var events = ValueList<Event<ReactedResult<IValidResult>>>.Empty;
            foreach (var action in actions)
            {
                var result = Context.Actions.Run(Simulating, action.Content);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            Result = Result with { ActorUpdates = events };
        }

        private void IncrementTurn()
        {
            var result = Context.Actions.IncrementTurn(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { IncrementTurn = @event };
        }
    }
}
