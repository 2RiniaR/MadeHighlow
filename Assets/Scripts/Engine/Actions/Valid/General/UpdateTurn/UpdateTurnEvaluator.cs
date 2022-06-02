using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.IncrementTurn;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public class UpdateTurnEvaluator
    {
        public UpdateTurnEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            [NotNull] UpdateTurnAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private UpdateTurnAction Action { get; }

        [CanBeNull] private ValueList<Interrupt<IValidAction>> ActorInterrupts { get; set; }
        [CanBeNull] [ItemNotNull] private ValueList<Event<ReactedResult<IValidResult>>> ActorEvents { get; set; }
        [CanBeNull] private Event<IncrementTurnResult> IncrementTurnEvent { get; set; }
        [CanBeNull] private UpdateTurnProcess Process { get; set; }

        [NotNull]
        public UpdateTurnResult Evaluate()
        {
            RunActions();
            IncrementTurn();
            WrapProcess();
            return Succeed();
        }

        private void RunActions()
        {
            var interruptsQueue = ValuePriorityQueue<Interrupt<IValidAction>>.Empty;
            var actors = Context.Finder.GetAllComponents<IUpdateTurnActor>(Simulating.World).Sort();
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
            Process = new UpdateTurnProcess(ActorEvents, IncrementTurnEvent);
        }

        private UpdateTurnResult Succeed()
        {
            return new UpdateTurnResult(Action, ActorInterrupts, Process);
        }
    }
}
