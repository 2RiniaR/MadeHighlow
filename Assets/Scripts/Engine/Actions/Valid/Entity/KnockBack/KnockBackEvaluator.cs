using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public class KnockBackEvaluator
    {
        public KnockBackEvaluator([NotNull] IHistory initial, [NotNull] KnockBackAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private KnockBackAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<KnockBackCalculation>> CalculationInterrupts { get; set; }
        [CanBeNull] private KnockBack Calculated { get; set; }

        [CanBeNull] private Event<MoveEntity.SucceedResult> ShiftMoveEvent { get; set; }
        [CanBeNull] private GroundElevation DestinationElevation { get; set; }
        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents { get; set; }
        [CanBeNull] private KnockBackProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<KnockBackRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public KnockBackResult Evaluate()
        {
            KnockBackResult result;
/*
            result = FindTarget();
            if (result != null) return result;

            CalculateKnockBack();

            result = MoveShift();
            if (result != null) return result;

            result = MoveFall();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;
*/
            return Succeed();
        }

/*
        [CanBeNull]
        private KnockBackResult FindTarget()
        {
            Contract.Ensures((Contract.Result<KnockBackResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        private void CalculateKnockBack()
        {
            Contract.Ensures(CalculationInterrupts != null);
            Contract.Ensures(Calculated != null);

            var effectors = Component.GetAllOfTypeFrom<IKnockBackCalculator>(Initial.World).Sort();

            CalculationInterrupts = ValueList<Interrupt<KnockBackCalculation>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.KnockBackCalculations(Initial, Action, CalculationInterrupts);
                if (interrupts == null) continue;
                CalculationInterrupts = CalculationInterrupts.AddRange(interrupts);
            }

            Calculated = Action.KnockBack;
            foreach (var interrupt in CalculationInterrupts)
            {
                if (interrupt.Effect is Reduction reduction)
                {
                    Calculated = reduction.Value.Caused(Calculated);
                }
            }
        }

        [CanBeNull]
        private KnockBackResult MoveShift()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures(
                (Contract.Result<KnockBackResult>() != null) ^ (ShiftMoveEvent != null && DestinationElevation != null)
            );

            var result = new MoveEntityAction(Action.TargetID, Action.Direction.To3D).Evaluate(Simulating);
            if (result is not MoveEntity.SucceedResult succeedResult)
            {
                return new ShiftFailedResult(Action, ClimbMoveEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            ShiftMoveEvent = succeedEvent;
            SteppingTarget = succeedResult.Process.PositionEntityEvent.Result.Positioned;

            return null;
        }

        [CanBeNull]
        private KnockBackResult MoveFall()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(SteppingTarget != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Requires<InvalidOperationException>(ClimbMoveEvents != null);
            Contract.Requires<InvalidOperationException>(ShiftMoveEvent != null);

            var distance = new Distance(SteppingTarget.Position3D.Z.Value - DestinationElevation.Height.Value);
            FallMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(Action.TargetID, Direction3D.ZPositive).Evaluate(Simulating);
                if (result is not MoveEntity.SucceedResult succeedResult)
                {
                    return new FallFailedResult(Action, ClimbMoveEvents, ShiftMoveEvent, FallMoveEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                FallMoveEvents = FallMoveEvents.Add(succeedEvent);
                SteppingTarget = succeedResult.Process.PositionEntityEvent.Result.Positioned;
            }

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(ClimbMoveEvents != null);
            Contract.Requires<InvalidOperationException>(ShiftMoveEvent != null);
            Contract.Requires<InvalidOperationException>(FallMoveEvents != null);
            Contract.Ensures(Process != null);

            Process = new EntityStepProcess(ClimbMoveEvents, ShiftMoveEvent, FallMoveEvents);
        }

        [CanBeNull]
        private KnockBackResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Ensures(RejectionInterrupts != null);

            var rejectors = Component.GetAllOfTypeFrom<IKnockBackRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<KnockBackRejection>>.Empty;
            foreach (var rejector in rejectors)
            {
                var interrupt = rejector.KnockBackRejection(Initial, Action, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(
                    Action,
                    CalculationInterrupts,
                    Calculated,
                    RejectionInterrupts,
                    RejectionInterrupts[0].ComponentID
                );
            }

            return null;
        }
*/
        [NotNull]
        private KnockBackResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(CalculationInterrupts != null);
            Contract.Requires<InvalidOperationException>(Calculated != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult();
        }
    }
}
