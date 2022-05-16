using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public interface IEntityStepEffector : IPriority<IEntityStepEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<EntityStepEffect>> EffectsOnEntityStep(
            [NotNull] IHistory session,
            [NotNull] EntityStepAction action,
            [NotNull] EntityStepProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityStepCostEffect>> costInterrupts,
            [NotNull] EntityStepCost expendedCost
        );
    }
}
