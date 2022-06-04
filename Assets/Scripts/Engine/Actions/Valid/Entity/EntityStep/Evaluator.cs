using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        [NotNull] private static readonly Cost ClimbCost = new(5);
        [NotNull] private static readonly Cost HorizontalCost = new(1);
        [NotNull] private static readonly Cost FallCost = new(0);

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Entity SteppingTarget { get; set; }
        [CanBeNull] private GroundElevation DestinationElevation { get; set; }

        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> ClimbMoveEvents { get; set; }
        [CanBeNull] private Event<MoveEntity.SucceedResult> ShiftMoveEvent { get; set; }
        [CanBeNull] private ValueList<Event<MoveEntity.SucceedResult>> FallMoveEvents { get; set; }
        [CanBeNull] private Process Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<CostEffect>> CostEffectInterrupts { get; set; }
        [CanBeNull] private Cost ExpendedCost { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

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

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(
                    history,
                    collected,
                    Action,
                    Process,
                    CostEffectInterrupts,
                    ExpendedCost
                ),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    Process,
                    CostEffectInterrupts,
                    ExpendedCost,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindActor()
        {
            Target = Context.Finder.FindEntity(Simulating.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private Result FindDestination()
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
        private Result MoveClimb()
        {
            var distance = new Distance(DestinationElevation.Height.Value - Target.Position3D.Z.Value);
            ClimbMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = Context.Actions.MoveEntity(
                    Simulating,
                    new MoveEntity.Action(Action.TargetID, Direction3D.ZPositive)
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
        private Result MoveShift()
        {
            var result = Context.Actions.MoveEntity(
                Simulating,
                new MoveEntity.Action(Action.TargetID, Action.Direction.To3D)
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
        private Result MoveFall()
        {
            var distance = new Distance(SteppingTarget.Position3D.Z.Value - DestinationElevation.Height.Value);
            FallMoveEvents = ValueList<Event<MoveEntity.SucceedResult>>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = Context.Actions.MoveEntity(
                    Simulating,
                    new MoveEntity.Action(Action.TargetID, Direction3D.ZPositive)
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
            Process = new Process(ClimbMoveEvents, ShiftMoveEvent, FallMoveEvents);
        }

        [NotNull]
        private static Cost BaseCost([NotNull] Process process)
        {
            return ClimbCost * process.ClimbMoveEvents.Count + HorizontalCost + FallCost * process.FallMoveEvents.Count;
        }

        [CanBeNull]
        private Result CheckCostIsEnough()
        {
            CostEffectInterrupts = ValueList<Interrupt<CostEffect>>.Empty;
            var effectors = Context.Finder.GetAllComponents<ICostEffector>(Simulating.World).Sort();
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

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process, CostEffectInterrupts, ExpendedCost);
        }
    }
}
