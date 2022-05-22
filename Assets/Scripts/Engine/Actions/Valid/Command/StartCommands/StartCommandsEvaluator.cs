using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public class StartCommandsEvaluator
    {
        public StartCommandsEvaluator([NotNull] IHistory initial, StartCommandsAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

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
            Contract.Ensures(RunCommandEvents != null);

            RunCommandEvents = ValueList<Event<ReactedResult<RunCommandResult>>>.Empty;
            var commands = Initial.World.ReservedCommands;
            var orderedCommands = new StartCommandsOrderer(commands).Resolve(Simulating);

            foreach (var command in orderedCommands)
            {
                var result = new RunCommandAction(command).Evaluate(Initial);
                Simulating = Simulating.Appended(result, out var @event);
                RunCommandEvents = RunCommandEvents.Add(@event);
            }
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(RunCommandEvents != null);
            Contract.Ensures(Process != null);

            Process = new StartCommandsProcess(RunCommandEvents);
        }

        [NotNull]
        private StartCommandsResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new StartCommandsResult(Action, Process);
        }
    }
}
