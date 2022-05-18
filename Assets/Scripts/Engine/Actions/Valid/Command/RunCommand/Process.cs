using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.PayCard;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public record Process(
        [NotNull] ValueList<Event<ReactedResult>> CommandActionEvents,
        [NotNull] Event<ReactedResult<PayCardResult>> PayCardEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(CommandActionEvents).Then(PayCardEvent);
    }
}
