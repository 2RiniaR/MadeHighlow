using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RunCommandProcess(
        [NotNull] ValueList<Event<ReactedResult<IValidResult>>> CommandActionEvents,
        [NotNull] Event<ReactedResult<PayCardResult>> PayCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(CommandActionEvents).Then(PayCardEvent);
    }
}
