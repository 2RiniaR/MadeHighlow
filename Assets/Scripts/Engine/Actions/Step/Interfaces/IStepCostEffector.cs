using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IStepCostEffector
    {
        public ValueObjectList<StepCostEffect> EffectStepCost(
            [NotNull] in IActionContext session,
            [NotNull] in EntityEnsuredID actor
        );
    }
}