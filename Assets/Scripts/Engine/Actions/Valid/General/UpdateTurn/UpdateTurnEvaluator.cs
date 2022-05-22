using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.IncrementTurn;

namespace RineaR.MadeHighlow.Actions.General.UpdateTurn
{
    public class UpdateTurnEvaluator
    {
        public UpdateTurnEvaluator([NotNull] IHistory initial, [NotNull] UpdateTurnAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private UpdateTurnAction Action { get; }

        [CanBeNull] private ValueList<Interrupt<ValidAction>> ActorInterrupts { get; set; }
        [CanBeNull] [ItemNotNull] private ValueList<Event<ReactedResult>> ActorEvents { get; set; }
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
            Contract.Ensures(ActorEvents != null);

            var interruptsQueue = ValuePriorityQueue<Interrupt<ValidAction>>.Empty;
            var actors = Component.GetAllOfTypeFrom<IUpdateTurnActor>(Simulating.World).Sort();
            foreach (var actor in actors)
            {
                var interrupts = actor.UpdateTurnActions(Simulating, Action, interruptsQueue.ToValueList());
                if (interrupts == null) continue;
                interruptsQueue = interruptsQueue.EnqueueRange(interrupts);
            }

            ActorInterrupts = interruptsQueue.ToValueList();

            ActorEvents = ValueList<Event<ReactedResult>>.Empty;
            foreach (var interrupt in ActorInterrupts)
            {
                var result = interrupt.Effect.EvaluateBase(Simulating);
                Simulating = Simulating.Appended(result, out var @event);
                ActorEvents = ActorEvents.Add(@event);
            }
        }

        private void IncrementTurn()
        {
            var result = new IncrementTurnAction().Evaluate(Simulating);
            Simulating = Simulating.Appended(result, out var @event);
            IncrementTurnEvent = @event;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(ActorEvents != null);
            Contract.Requires<InvalidOperationException>(IncrementTurnEvent != null);
            Contract.Ensures(Process != null);

            Process = new UpdateTurnProcess(ActorEvents, IncrementTurnEvent);
        }

        private UpdateTurnResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(ActorInterrupts != null);
            Contract.Requires<InvalidOperationException>(Process != null);

            return new UpdateTurnResult(Action, ActorInterrupts, Process);
        }
    }
}
