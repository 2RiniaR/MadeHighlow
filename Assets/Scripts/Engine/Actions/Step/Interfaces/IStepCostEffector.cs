using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IStepCostEffector
    {
        public ValueList<StepCostEffect> EffectStepCost([NotNull] IActionContext session, [NotNull] EntityID actor);
    }
}
