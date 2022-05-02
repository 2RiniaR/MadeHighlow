using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepCostEffector
    {
        public ValueObjectList<StepCostEffect> EffectStepCost(
            [NotNull] in ISessionModel session,
            [NotNull] in EntityLocator actor,
            [NotNull] in EntityLocator reactor
        );
    }
}