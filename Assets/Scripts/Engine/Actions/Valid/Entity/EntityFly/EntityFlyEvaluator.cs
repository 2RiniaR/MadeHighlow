using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public class EntityFlyEvaluator
    {
        public EntityFlyEvaluator(
            [NotNull] IHistory history,
            [NotNull] EntityID targetID,
            [NotNull] Direction3D direction
        )
        {
            History = history;
            TargetID = targetID;
            Direction = direction;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private EntityID TargetID { get; }
        [NotNull] private Direction3D Direction { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityFlyEffect>> Interrupts { get; set; }
        [CanBeNull] private Fragment.MoveEntity.SucceedResult MoveEntityResult { get; set; }

        [NotNull]
        public EntityFlyResult Evaluate()
        {
            EntityFlyResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CheckCanFly();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = Move();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private EntityFlyResult GetTarget()
        {
            Contract.Ensures((Contract.Result<EntityFlyResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private EntityFlyResult CheckCanFly()
        {
            Contract.Requires<InvalidOperationException>(Target != null);

            if (Target.Levitation == false)
            {
                return new CanNotFlyResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private EntityFlyResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IEntityFlyEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnFlyEntity(History, Target, Direction))
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(TargetID, Direction, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [CanBeNull]
        private EntityFlyResult Move()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Ensures((Contract.Result<EntityFlyResult>() != null) ^ (MoveEntityResult != null));

            var result = new MoveEntityAction(TargetID, Direction).Evaluate(History);
            if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
            {
                return new MoveFailedResult(TargetID, Direction, Interrupts, result);
            }

            MoveEntityResult = succeedResult;
            return null;
        }

        [NotNull]
        private EntityFlyResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(MoveEntityResult != null);

            return new SucceedResult(Interrupts, MoveEntityResult);
        }
    }
}
