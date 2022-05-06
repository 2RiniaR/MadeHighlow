using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     複数のユニットが一斉に現在受けている命令を実行するアクション
    /// </summary>
    public record StartCommandsAction : Action<StartCommandsResult>
    {
        public override StartCommandsResult Validate(in IActionContext context)
        {
            var currentContext = context;

            var results = new List<RunCommandResult>();
            foreach (var command in OrderedCommands(context))
            {
                var result = new RunCommandAction(command).Validate(context);
                currentContext = currentContext.Appended(result);
                results.Add(result);
            }

            return new StartCommandsResult(results.ToValueObjectList());
        }

        [NotNull]
        [ItemNotNull]
        private ValueObjectList<Command> OrderedCommands([NotNull] in IActionContext context)
        {
            return new StartCommandsOrderer(context.World.ReservedCommands).Resolve(context);
        }
    }
}