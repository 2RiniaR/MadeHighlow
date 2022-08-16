namespace RineaR.MadeHighlow.GameModel.Events
{
    public class RunCommandLog : IEventLog
    {
        public ICommandResult CommandResult { get; init; }
    }
}