using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IAdvanceActor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<Action> AdvanceActionsOn([NotNull] Action action);
    }
}
