using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public class EntityTeleportEvaluator
    {
        public EntityTeleportEvaluator(
            [NotNull] IHistory history,
            [NotNull] EntityID targetID,
            [NotNull] Position3D destination
        )
        {
            History = history;
            TargetID = targetID;
            Destination = destination;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private EntityID TargetID { get; }
        [NotNull] private Position3D Destination { get; }
        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityTeleportEffect>> Interrupts { get; set; }
        [CanBeNull] private Fragment.PositionEntity.SucceedResult PositionEntityResult { get; set; }

        [NotNull]
        public EntityTeleportResult Evaluate()
        {
            EntityTeleportResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private EntityTeleportResult GetTarget()
        {
            Contract.Ensures((Contract.Result<EntityTeleportResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private EntityTeleportResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IEntityTeleportEffector>(History.World);
            Interrupts = effectors
                .SelectMany(effector => effector.EffectsOnTeleportEntity(History, Target, Destination))
                .Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(TargetID, Destination, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [CanBeNull]
        private EntityTeleportResult Position()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Ensures((Contract.Result<EntityTeleportResult>() != null) ^ (PositionEntityResult != null));

            var result = new PositionEntityAction(TargetID, Destination).Evaluate(History);
            if (result is not Fragment.PositionEntity.SucceedResult succeedResult)
            {
                return new PositionFailedResult(TargetID, Destination, Interrupts, result);
            }

            PositionEntityResult = succeedResult;
            return null;
        }

        [NotNull]
        private EntityTeleportResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);
            Contract.Requires<InvalidOperationException>(PositionEntityResult != null);

            return new SucceedResult(Interrupts, PositionEntityResult);
        }
    }
}
