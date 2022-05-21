using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public class EntityStepEvaluator
    {
        public EntityStepEvaluator([NotNull] IHistory initial, EntityStepAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private EntityStepAction Action { get; }

        [NotNull] private static readonly EntityStepCost ClimbCost = new(5);
        [NotNull] private static readonly EntityStepCost HorizontalCost = new(1);
        [NotNull] private static readonly EntityStepCost FallCost = new(0);

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Entity SteppingTarget { get; set; }
        [CanBeNull] private GroundElevation DestinationElevation { get; set; }

        [CanBeNull] private ValueList<Event<Fragment.MoveEntity.SucceedResult>> ClimbMoveEvents { get; set; }
        [CanBeNull] private Event<Fragment.MoveEntity.SucceedResult> ShiftMoveEvent { get; set; }
        [CanBeNull] private ValueList<Event<Fragment.MoveEntity.SucceedResult>> FallMoveEvents { get; set; }
        [CanBeNull] private EntityStepProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<EntityStepCostEffect>> CostEffectInterrupts { get; set; }
        [CanBeNull] private EntityStepCost ExpendedCost { get; set; }

        [CanBeNull] private ValueList<Interrupt<EntityStepRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public EntityStepResult Evaluate()
        {
            EntityStepResult result;

            result = FindActor();
            if (result != null) return result;

            result = FindDestination();
            if (result != null) return result;

            result = MoveClimb();
            if (result != null) return result;

            result = MoveShift();
            if (result != null) return result;

            result = MoveFall();
            if (result != null) return result;

            WrapProcess();

            result = CheckCostIsEnough();
            if (result != null) return result;

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private EntityStepResult FindActor()
        {
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Simulating.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult FindDestination()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (DestinationElevation != null));

            var position = Target.Position3D.To2D().MoveTo(Action.Direction, new Distance(1));
            var destination = position.GetTile(Simulating.World);

            if (destination == null ||
                destination.Elevation is not GroundElevation groundElevation ||
                groundElevation.Placeable == false)
            {
                return new DestinationNotFoundResult(Action);
            }

            DestinationElevation = groundElevation;
            return null;
        }

        [CanBeNull]
        private EntityStepResult MoveClimb()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Ensures(ClimbMoveEvents != null);

            var distance = new Distance(DestinationElevation.Height.Value - Target.Position3D.Z.Value);
            ClimbMoveEvents = ValueList<Event<Fragment.MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(Action.TargetID, Direction3D.ZPositive).Evaluate(Simulating);
                if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
                {
                    return new ClimbFailedResult(Action, ClimbMoveEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                ClimbMoveEvents = ClimbMoveEvents.Add(succeedEvent);
                SteppingTarget = succeedResult.Process.PositionEntityEvent.Result.Positioned;
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult MoveShift()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(ClimbMoveEvents != null);
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (ShiftMoveEvent != null));

            var result = new MoveEntityAction(Action.TargetID, Action.Direction.To3D).Evaluate(Simulating);
            if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
            {
                return new ShiftFailedResult(Action, ClimbMoveEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            ShiftMoveEvent = succeedEvent;
            SteppingTarget = succeedResult.Process.PositionEntityEvent.Result.Positioned;

            return null;
        }

        [CanBeNull]
        private EntityStepResult MoveFall()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(SteppingTarget != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Requires<InvalidOperationException>(ClimbMoveEvents != null);
            Contract.Requires<InvalidOperationException>(ShiftMoveEvent != null);

            var distance = new Distance(SteppingTarget.Position3D.Z.Value - DestinationElevation.Height.Value);
            FallMoveEvents = ValueList<Event<Fragment.MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(Action.TargetID, Direction3D.ZPositive).Evaluate(Simulating);
                if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
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

        [NotNull]
        private static EntityStepCost BaseCost([NotNull] EntityStepProcess process)
        {
            return ClimbCost * process.ClimbMoveEvents.Count + HorizontalCost + FallCost * process.FallMoveEvents.Count;
        }

        [CanBeNull]
        private EntityStepResult CheckCostIsEnough()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(CostEffectInterrupts != null);
            Contract.Ensures(ExpendedCost != null);

            CostEffectInterrupts = ValueList<Interrupt<EntityStepCostEffect>>.Empty;
            var effectors = Component.GetAllOfTypeFrom<IEntityStepCostEffector>(Simulating.World).Sort();
            foreach (var effector in effectors)
            {
                var interrupts = effector.EntityStepCostEffects(Simulating, Action, Process, CostEffectInterrupts);
                if (interrupts == null) continue;
                CostEffectInterrupts = CostEffectInterrupts.AddRange(interrupts);
            }

            CostEffectInterrupts = CostEffectInterrupts.Sort();
            ExpendedCost = BaseCost(Process);
            foreach (var costInterrupt in CostEffectInterrupts)
            {
                if (costInterrupt.Effect is CostAdditionEffect addition)
                {
                    ExpendedCost += addition.Value;
                }
                else if (costInterrupt.Effect is CostReductionEffect reduction)
                {
                    ExpendedCost -= reduction.Value;
                }
                else if (costInterrupt.Effect is CostOverwriteEffect overwrite)
                {
                    ExpendedCost = overwrite.Value;
                }
            }

            if (Action.Available > ExpendedCost)
            {
                return new CostOverResult(Action, Process, CostEffectInterrupts, ExpendedCost);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostEffectInterrupts != null);
            Contract.Requires<InvalidOperationException>(ExpendedCost != null);
            Contract.Ensures(RejectionInterrupts != null);

            RejectionInterrupts = ValueList<Interrupt<EntityStepRejection>>.Empty;
            var effectors = Component.GetAllOfTypeFrom<IEntityStepRejector>(Simulating.World).Sort();
            foreach (var effector in effectors)
            {
                var interrupt = effector.EntityStepRejection(
                    Simulating,
                    Action,
                    Process,
                    CostEffectInterrupts,
                    ExpendedCost,
                    RejectionInterrupts
                );
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(
                    Action,
                    Process,
                    CostEffectInterrupts,
                    ExpendedCost,
                    RejectionInterrupts,
                    RejectionInterrupts[0].ComponentID
                );
            }

            return null;
        }

        [NotNull]
        private EntityStepResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostEffectInterrupts != null);
            Contract.Requires<InvalidOperationException>(ExpendedCost != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, CostEffectInterrupts, ExpendedCost, RejectionInterrupts);
        }
    }
}
