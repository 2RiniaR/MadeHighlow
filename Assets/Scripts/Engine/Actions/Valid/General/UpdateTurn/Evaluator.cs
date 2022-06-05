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
        [NotNull] private Result Result { get; }

        [NotNull]
        public Result Evaluate()
        {
            RunActions();
            IncrementTurn();
            WrapProcess();
            return Succeed();
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

            ActorInterrupts = interruptsQueue.ToValueList();

            ActorEvents = ValueList<Event<ReactedResult<IValidResult>>>.Empty;
            foreach (var interrupt in ActorInterrupts)
            {
                var result = Context.Actions.Run(Simulating, interrupt.Effect);
                Simulating = Simulating.Appended(result, out var @event);
                ActorEvents = ActorEvents.Add(@event);
            }
        }

        private void IncrementTurn()
        {
            var result = Context.Actions.IncrementTurn(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            IncrementTurnEvent = @event;
        }

        private void WrapProcess()
        {
            Process = new Process(ActorEvents, IncrementTurnEvent);
        }

        private Result Succeed()
        {
            return new Result(Action, ActorInterrupts, Process);
        }
    }
}
