using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public record Process(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> ActorEvents,
        [NotNull] Event<IncrementTurn.Result> IncrementTurnEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(ActorEvents).Then(IncrementTurnEvent);
    }
}
