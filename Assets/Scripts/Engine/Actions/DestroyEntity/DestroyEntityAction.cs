using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DestroyEntityAction([NotNull] EntityID TargetID) : Action<DestroyEntityResult>
    {
        public override DestroyEntityResult Evaluate(IActionContext context)
        {
            var target = TargetID.GetFrom(context.World);
            if (target == null)
            {
                return new NotFoundResult(TargetID);
            }

            var removeComponents = ValueList<RemoveComponent.SucceedResult>.Empty;
            foreach (var component in target.Components)
            {
                var result = new RemoveComponentAction(component.ComponentID).Evaluate(context);
                if (result is not RemoveComponent.SucceedResult succeedResult)
                {
                    return new RemoveComponentFailedResult(target, removeComponents, result);
                }

                removeComponents = removeComponents.Add(succeedResult);
            }

            var effectors = Component.GetAllOfTypeFrom<IDestroyEntityEffector>(context.World);
            var interrupts = effectors.SelectMany(effector => effector.EffectsOnGenerateEntity(context, target)).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(target, interrupt.ComponentID, removeComponents, interrupts);
                }
            }

            return new SucceedResult(target, removeComponents, interrupts);
        }
    }
}
