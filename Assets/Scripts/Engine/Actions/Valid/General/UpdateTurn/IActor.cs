using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UpdateTurn
{
    public interface IActor : IPriority<IActor>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<IValidAction>> UpdateTurnActions(
            [NotNull] IHistory history,
            [NotNull] Action action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<IValidAction>> collected
        );
    }
}
