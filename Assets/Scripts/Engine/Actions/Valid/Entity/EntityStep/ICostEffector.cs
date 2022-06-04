using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public interface ICostEffector : IPriority<ICostEffector>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<CostEffect>> EntityStepCostEffects(
            [NotNull] IHistory session,
            [NotNull] Action action,
            [NotNull] Process process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<CostEffect>> collected
        );
    }
}
