using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public class StartCommandsEvaluator
    {
        public StartCommandsEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            StartCommandsAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private StartCommandsAction Action { get; }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<ReactedResult<RunCommandResult>>> RunCommandEvents { get; set; }

        [CanBeNull] private StartCommandsProcess Process { get; set; }

        [NotNull]
        public StartCommandsResult Evaluate()
        {
            RunByOrder();
            WrapProcess();
            return Succeed();
        }

        private void RunByOrder()
        {
            RunCommandEvents = ValueList<Event<ReactedResult<RunCommandResult>>>.Empty;
            var commands = Initial.World.ReservedCommands;
            var orderedCommands = new StartCommandsOrderer(Context).Resolve(Simulating, commands);

            foreach (var command in orderedCommands)
            {
                var result = Context.Actions.RunCommand(Initial, new RunCommandAction(command));
                Simulating = Simulating.Appended(result, out var @event);
                RunCommandEvents = RunCommandEvents.Add(@event);
            }
        }

        private void WrapProcess()
        {
            Process = new StartCommandsProcess(RunCommandEvents);
        }

        [NotNull]
        private StartCommandsResult Succeed()
        {
            return new StartCommandsResult(Action, Process);
        }
    }
}
