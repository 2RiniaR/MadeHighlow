using System.Collections.Generic;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    /// <summary>
    ///     複数のユニットが一斉に現在受けている命令を実行するアクション
    /// </summary>
    public record StartCommandsAction : Action<StartCommandsResult>
    {
        public override StartCommandsResult Evaluate(IActionContext context)
        {
            var currentContext = context;

            var results = new List<RunCommandResult>();
            foreach (var command in OrderedCommands(context))
            {
                var result = new RunCommandAction(command).Evaluate(context);
                currentContext = currentContext.Appended(result);
                results.Add(result);
            }

            return new StartCommandsResult(results.ToValueList());
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<Command> OrderedCommands([NotNull] IActionContext context)
        {
            return new StartCommandsOrderer(context.World.ReservedCommands).Resolve(context);
        }
    }
}
