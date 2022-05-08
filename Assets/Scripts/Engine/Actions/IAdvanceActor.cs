using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAdvanceActor
    {
        [ItemNotNull]
        [NotNull]
        public ValueList<Action> AdvanceActionsOn([NotNull] Action action);
    }
}
