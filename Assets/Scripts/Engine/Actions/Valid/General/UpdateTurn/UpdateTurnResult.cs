using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.General.UpdateTurn
{
    public record UpdateTurnResult(
        [NotNull] UpdateTurnAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<IValidAction>> ActorInterrupts,
        [NotNull] UpdateTurnProcess Process
    ) : ValidResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
