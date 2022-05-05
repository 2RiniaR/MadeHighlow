using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IAdvanceActor
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<Action> AdvanceActionsOn([NotNull] Action action);
    }
}