using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public class StartCommandsEvaluator
    {
        public StartCommandsEvaluator([NotNull] IHistory history)
        {
            History = history;
        }

        [NotNull] private IHistory History { get; set; }

        [CanBeNull] [ItemNotNull] private ValueList<ReactedResult<RunCommandResult>> RunCommandResults { get; set; }

        [NotNull]
        public StartCommandsResult Evaluate()
        {
            RunByOrder();
            return Succeed();
        }

        private void RunByOrder()
        {
            Contract.Ensures(RunCommandResults != null);

            RunCommandResults = ValueList<ReactedResult<RunCommandResult>>.Empty;
            var commands = History.World.ReservedCommands;
            var orderedCommands = new StartCommandsOrderer(commands).Resolve(History);

            foreach (var command in orderedCommands)
            {
                var result = new RunCommandAction(command).Evaluate(History);
                History = History.Appended(result);
                RunCommandResults = RunCommandResults.Add(result);
            }
        }

        [NotNull]
        private StartCommandsResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RunCommandResults != null);

            return new StartCommandsResult(RunCommandResults);
        }
    }
}
