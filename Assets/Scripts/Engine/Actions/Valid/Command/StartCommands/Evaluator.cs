using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<ReactedResult<RunCommand.Result>>> RunCommandEvents { get; set; }

        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            RunByOrder();
            WrapProcess();
            return Succeed();
        }

        private void RunByOrder()
        {
            RunCommandEvents = ValueList<Event<ReactedResult<RunCommand.Result>>>.Empty;
            var commands = Initial.World.ReservedCommands;
            var orderedCommands = new Orderer(Context).Resolve(Simulating, commands);

            foreach (var command in orderedCommands)
            {
                var result = Context.Actions.RunCommand(Initial, new RunCommand.Action(command));
                Simulating = Simulating.Appended(result, out var @event);
                RunCommandEvents = RunCommandEvents.Add(@event);
            }
        }

        private void WrapProcess()
        {
            Process = new Process(RunCommandEvents);
        }

        [NotNull]
        private Result Succeed()
        {
            return new Result(Action, Process);
        }
    }
}
