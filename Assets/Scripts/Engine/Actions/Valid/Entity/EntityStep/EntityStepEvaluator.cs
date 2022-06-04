using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.MoveEntity;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public class EntityStepEvaluator
    {
        public EntityStepEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            EntityStepAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private EntityStepAction Action { get; }

        [NotNull] private static readonly EntityStepCost ClimbCost = new(5);
        [NotNull] private static readonly EntityStepCost HorizontalCost = new(1);
        [NotNull] private static readonly EntityStepCost FallCost = new(0);

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Entity SteppingTarget { get; set; }
        [CanBeNull] private GroundElevation DestinationElevation { get; set; }

        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents { get; set; }
        [CanBeNull] private Event<MoveEntity.SucceedResult> ShiftMoveEvent { get; set; }
        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents { get; set; }
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
            Target = Context.Finder.FindEntity(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult FindDestination()
        {
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
            var distance = new Distance(DestinationElevation.Height.Value - Target.Position3D.Z.Value);
            ClimbMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = Context.Actions.MoveEntity(
                    Simulating,
                    new MoveEntityAction(Action.TargetID, Direction3D.ZPositive)
                );
                if (result is not MoveEntity.SucceedResult succeedResult)
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
            var result = Context.Actions.MoveEntity(
                Simulating,
                new MoveEntityAction(Action.TargetID, Action.Direction.To3D)
            );
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
        private EntityStepResult MoveFall()
        {
            var distance = new Distance(SteppingTarget.Position3D.Z.Value - DestinationElevation.Height.Value);
            FallMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = Context.Actions.MoveEntity(
                    Simulating,
                    new MoveEntityAction(Action.TargetID, Direction3D.ZPositive)
                );
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
            CostEffectInterrupts = ValueList<Interrupt<EntityStepCostEffect>>.Empty;
            var effectors = Context.Finder.GetAllComponents<IEntityStepCostEffector>(Simulating.World).Sort();
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
            RejectionInterrupts = ValueList<Interrupt<EntityStepRejection>>.Empty;
            var effectors = Context.Finder.GetAllComponents<IEntityStepRejector>(Simulating.World).Sort();
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
            return new SucceedResult(Action, Process, CostEffectInterrupts, ExpendedCost, RejectionInterrupts);
        }
    }
}
