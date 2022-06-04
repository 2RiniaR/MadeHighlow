using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record Process(
        [NotNull] ValueList<Event<ReactedResult<IValidResult>>> CommandActionEvents,
        [NotNull] Event<ReactedResult<PayCard.Result>> PayCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(CommandActionEvents).Then(PayCardEvent);
    }
}
