using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RunCommand;

namespace RineaR.MadeHighlow.Actions.Valid.StartCommands
{
    public record Process([NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RunCommandResult>>> RunCommandEvents)
    {
        public Timeline Timeline { get; } = new Timeline().Then(RunCommandEvents);
    }
}
