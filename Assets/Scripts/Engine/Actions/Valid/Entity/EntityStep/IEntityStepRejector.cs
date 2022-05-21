using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public interface IEntityStepRejector : IPriority<IEntityStepRejector>
    {
        [NotNull]
        public Interrupt<EntityStepRejection> EntityStepRejection(
            [NotNull] IHistory session,
            [NotNull] EntityStepAction action,
            [NotNull] EntityStepProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> costEffectInterrupts,
            [NotNull] EntityStepCost expendedCost,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepRejection>> collected
        );
    }
}
