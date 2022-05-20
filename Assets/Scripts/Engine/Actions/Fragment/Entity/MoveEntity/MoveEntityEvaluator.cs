using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public class MoveEntityEvaluator
    {
        public MoveEntityEvaluator([NotNull] IHistory initial, MoveEntityAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private MoveEntityAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<MoveEntityEffect>> Interrupts { get; set; }
        [CanBeNull] private Event<PositionEntity.SucceedResult> PositionEntityEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [NotNull]
        public MoveEntityResult Evaluate()
        {
            MoveEntityResult result;

            result = FindTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            WrapProcess();
            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private MoveEntityResult FindTarget()
        {
            Contract.Ensures((Contract.Result<MoveEntityResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private MoveEntityResult Position()
        {
            Contract.Ensures((Contract.Result<MoveEntityResult>() != null) ^ (PositionEntityEvent != null));
            Contract.Requires<InvalidOperationException>(Target != null);

            var result = new PositionEntityAction(
                Action.TargetID,
                Target.Position3D.MoveTo(Action.Direction, new Distance(1))
            ).Evaluate(Simulating);
            if (result is not PositionEntity.SucceedResult succeedResult)
            {
                return new PositionFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            PositionEntityEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(PositionEntityEvent != null);
            Contract.Ensures(Process != null);

            Process = new Process(PositionEntityEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IMoveEntityEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<MoveEntityEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnMoveEntity(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private MoveEntityResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Action, Process, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private MoveEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
