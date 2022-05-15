using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public class MoveEntityEvaluator
    {
        public MoveEntityEvaluator(
            [NotNull] IHistory history,
            [NotNull] EntityID targetID, // TODO: 何度もEntityを取得し直すと非効率だから、Entity | EntityID みたいなのが欲しい
            [NotNull] Direction3D direction
        )
        {
            History = history;
            TargetID = targetID;
            Direction = direction;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private EntityID TargetID { get; }
        [NotNull] public Direction3D Direction { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<MoveEntityEffect>> Interrupts { get; set; }
        [CanBeNull] private PositionEntity.SucceedResult PositionEntityResult { get; set; }

        [NotNull]
        public MoveEntityResult Evaluate()
        {
            MoveEntityResult result;

            result = GetTarget();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private MoveEntityResult GetTarget()
        {
            Contract.Ensures((Contract.Result<MoveEntityResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private MoveEntityResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IMoveEntityEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnMoveEntity(History, Target, Direction))
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
        private MoveEntityResult Position()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<MoveEntityResult>() != null) ^ (PositionEntityResult != null));

            var result = new PositionEntityAction(TargetID, Target.Position3D.MoveTo(Direction, new Distance(1)))
                .Evaluate(History);
            if (result is not PositionEntity.SucceedResult succeedResult)
            {
                return new PositionFailedResult(TargetID, Direction, result);
            }

            PositionEntityResult = succeedResult;
            return null;
        }

        [NotNull]
        private MoveEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(PositionEntityResult != null);

            return new SucceedResult(PositionEntityResult);
        }
    }
}
