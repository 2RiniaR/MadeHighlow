using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.General.UpdateTurn
{
    public interface IUpdateTurnActor : IPriority<IUpdateTurnActor>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<ValidAction>> UpdateTurnActions(
            [NotNull] IHistory history,
            [NotNull] UpdateTurnAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<ValidAction>> collected
        );
    }
}
