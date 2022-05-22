using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsProcess(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RunCommandResult>>> RunCommandEvents
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(RunCommandEvents);
    }
}
