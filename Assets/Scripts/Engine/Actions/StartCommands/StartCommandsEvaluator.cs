using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public class StartCommandsEvaluator
    {
        public StartCommandsEvaluator([NotNull] IActionContext context)
        {
            Context = context;
        }

        [NotNull] private IActionContext Context { get; set; }

        [CanBeNull] [ItemNotNull] private ValueList<RunCommandResult> RunCommandResults { get; set; }

        [NotNull]
        public StartCommandsResult Evaluate()
        {
            RunByOrder();
            return Succeed();
        }

        private void RunByOrder()
        {
            Contract.Ensures(RunCommandResults != null);

            RunCommandResults = ValueList<RunCommandResult>.Empty;
            var commands = Context.World.ReservedCommands;
            var orderedCommands = new StartCommandsOrderer(commands).Resolve(Context);

            foreach (var command in orderedCommands)
            {
                var result = new RunCommandAction(command).Evaluate(Context);
                Context = Context.Appended(result);
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
