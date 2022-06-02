using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public interface IUpdateTurnActor : IPriority<IUpdateTurnActor>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<IValidAction>> UpdateTurnActions(
            [NotNull] IHistory history,
            [NotNull] UpdateTurnAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<IValidAction>> collected
        );
    }
}
