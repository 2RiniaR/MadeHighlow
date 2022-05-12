using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record RemoveComponentAction([NotNull] ComponentID TargetID) : Action<RemoveComponentResult>
    {
        public override RemoveComponentResult Evaluate(IActionContext context)
        {
            var target = TargetID.GetFrom(context.World);
            if (target == null)
            {
                return new NotFoundResult(TargetID);
            }

            var effectors = Component.GetAllOfTypeFrom<IRemoveComponentEffector>(context.World);
            var interrupts = effectors.SelectMany(effector => effector.EffectsOnRemoveComponent(context, target))
                .Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(target, interrupt.ComponentID);
                }
            }

            return new SucceedResult(target);
        }
    }
}
