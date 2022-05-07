using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IStepCostEffector
    {
        public ValueObjectList<StepCostEffect> EffectStepCost(
            [NotNull] IActionContext session,
            [NotNull] EntityID actor
        );
    }
}
