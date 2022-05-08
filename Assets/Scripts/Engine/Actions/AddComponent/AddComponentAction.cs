using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネントを追加するアクション
    /// </summary>
    public record AddComponentAction(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component Component
    ) : Action<AddComponentResult>
    {
        public override AddComponentResult Validate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectAddComponentEffect)
                {
                    return new RejectedAddComponentResult(Component, interrupt.ComponentID);
                }
            }

            return new SucceedAddComponentResult(Component);
        }

        [CanBeNull]
        private AddComponentResult PreValidationResult([NotNull] IActionContext context)
        {
            var target = TargetID.GetFrom(context.World);

            if (target == null)
            {
                return new FailedAddComponentResult(Component, FailedAddComponentReason.TargetNotFound);
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
