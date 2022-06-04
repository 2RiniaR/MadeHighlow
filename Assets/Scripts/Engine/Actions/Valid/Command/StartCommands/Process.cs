using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record Process([NotNull] [ItemNotNull] ValueList<Event<ReactedResult<RunCommand.Result>>> RunCommandEvents)
    {
        public Timeline Timeline { get; } = new Timeline().Then(RunCommandEvents);
    }
}
