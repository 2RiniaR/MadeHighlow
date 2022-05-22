using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.IncrementTurn;

namespace RineaR.MadeHighlow.Actions.General.UpdateTurn
{
    public record UpdateTurnProcess(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult>> ActorEvents,
        [NotNull] Event<IncrementTurnResult> IncrementTurnEvent
    )
    {
        public Timeline Timeline { get; } = new Timeline().Then(ActorEvents).Then(IncrementTurnEvent);
    }
}
