using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record GenerateEntityAction([NotNull] Entity Status) : Action<GenerateEntityResult>
    {
        public override GenerateEntityResult Evaluate(IActionContext context)
        {
            var processRunner = new ProcessRunner(this, ref context);
            if (!processRunner.TryProcess())
            {
                return new ProcessFailedResult(Status, processRunner.Failed ?? throw new NullReferenceException());
            }

            var process = processRunner.Succeed ?? throw new NullReferenceException();

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Status, interrupt.ComponentID, process, interrupts);
                }
            }

            // `RegisterEntity` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            var entityID = process.RegisterEntity.RegisteredEntity.EntityID;
            var entity = entityID.GetFrom(context.World);
            if (entity == null)
            {
                return new FailedResult(Status, process, interrupts, FailedReason.TargetDestroyed);
            }

            return new SucceedResult(Status, entity, process, interrupts);
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<GenerateEntityEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IGenerateEntityEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnGenerateEntity(context, this));
        }
    }
}
