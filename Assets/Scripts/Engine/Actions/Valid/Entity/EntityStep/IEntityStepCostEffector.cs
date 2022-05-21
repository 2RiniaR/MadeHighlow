using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public interface IEntityStepCostEffector : IPriority<IEntityStepCostEffector>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<EntityStepCostEffect>> EntityStepCostEffects(
            [NotNull] IHistory session,
            [NotNull] EntityStepAction action,
            [NotNull] EntityStepProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> collected
        );
    }
}
