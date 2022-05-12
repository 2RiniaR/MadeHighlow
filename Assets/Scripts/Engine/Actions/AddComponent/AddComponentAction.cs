using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component Component
    ) : Action<AddComponentResult>
    {
        public override AddComponentResult Evaluate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Component, interrupt.ComponentID);
                }
            }

            return new SucceedResult(Component);
        }

        [CanBeNull]
        private AddComponentResult PreValidationResult([NotNull] IActionContext context)
        {
            var target = TargetID.GetFrom(context.World);

            if (target == null)
            {
                return new FailedResult(Component, FailedReason.TargetNotFound);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<AddComponentEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IAddComponentEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnAddComponent(context, this));
        }
    }
}
