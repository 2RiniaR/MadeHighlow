using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityStep
{
    public class EntityStepEvaluator
    {
        public EntityStepEvaluator([NotNull] IHistory history, [NotNull] EntityStepAction action)
        {
            History = history;
            Action = action;
        }

        private static readonly EntityStepCost ClimbCost = new(5);
        private static readonly EntityStepCost HorizontalCost = new(1);
        private static readonly EntityStepCost FallCost = new(0);

        [NotNull] private IHistory History { get; set; }
        [NotNull] private EntityStepAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Entity SteppingTarget { get; set; }
        [CanBeNull] private GroundElevation DestinationElevation { get; set; }

        [CanBeNull] private ValueList<Fragment.MoveEntity.SucceedResult> ClimbResults { get; set; }
        [CanBeNull] private Fragment.MoveEntity.SucceedResult ShiftResult { get; set; }
        [CanBeNull] private ValueList<Fragment.MoveEntity.SucceedResult> FallResults { get; set; }
        [CanBeNull] private EntityStepProcess Process { get; set; }

        [CanBeNull] private ValueList<Interrupt<EntityStepCostEffect>> CostInterrupts { get; set; }
        [CanBeNull] private EntityStepCost ExpendedCost { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityStepEffect>> Interrupts { get; set; }

        [NotNull]
        public EntityStepResult Evaluate()
        {
            var result = UpdateHistory();
            if (result != null) return result;

            return Validate();
        }

        [CanBeNull]
        private EntityStepResult UpdateHistory()
        {
            EntityStepResult result;

            result = FindActor();
            if (result != null) return result;

            result = FindDestination();
            if (result != null) return result;

            result = Climb();
            if (result != null) return result;

            result = Shift();
            if (result != null) return result;

            result = Fall();
            if (result != null) return result;

            return null;
        }

        [NotNull]
        private EntityStepResult Validate()
        {
            EntityStepResult result;

            CollectCostInterrupts();
            CalculateCost();

            result = CheckCostIsEnough();
            if (result != null) return result;

            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private EntityStepResult FindActor()
        {
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action.TargetID);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult FindDestination()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (DestinationElevation != null));

            var position = Target.Position3D.To2D().MoveTo(Action.Direction, new Distance(1));
            var destination = position.GetTile(History.World);
            if (destination == null ||
                destination.Elevation is not GroundElevation groundElevation ||
                groundElevation.Placeable == false)
            {
                return new DestinationNotFoundResult(position);
            }

            DestinationElevation = groundElevation;
            return null;
        }

        [CanBeNull]
        private EntityStepResult Climb()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Ensures(ClimbResults != null);

            var distance = new Distance(DestinationElevation.Height.Value - Target.Position3D.Z.Value);
            ClimbResults = ValueList<Fragment.MoveEntity.SucceedResult>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(Action.TargetID, Direction3D.ZPositive).Evaluate(History);
                if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
                {
                    return new ClimbFailedResult(Action, ClimbResults, result);
                }

                ClimbResults = ClimbResults.Add(succeedResult);
                SteppingTarget = succeedResult.PositionEntityResult.Positioned;
                History = History.Appended(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult Shift()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(ClimbResults != null);
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (ShiftResult != null));

            var result = new MoveEntityAction(Action.TargetID, Action.Direction.To3D).Evaluate(History);
            if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
            {
                return new ShiftFailedResult(Action, ClimbResults, result);
            }

            ShiftResult = succeedResult;
            SteppingTarget = succeedResult.PositionEntityResult.Positioned;
            return null;
        }

        [CanBeNull]
        private EntityStepResult Fall()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(SteppingTarget != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Requires<InvalidOperationException>(ClimbResults != null);
            Contract.Requires<InvalidOperationException>(ShiftResult != null);

            var distance = new Distance(DestinationElevation.Height.Value - SteppingTarget.Position3D.Z.Value);
            FallResults = ValueList<Fragment.MoveEntity.SucceedResult>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(Action.TargetID, Direction3D.ZNegative).Evaluate(History);
                if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
                {
                    return new FallFailedResult(Action, ClimbResults, ShiftResult, FallResults, result);
                }

                FallResults = FallResults.Add(succeedResult);
                SteppingTarget = succeedResult.PositionEntityResult.Positioned;
                History = History.Appended(succeedResult);
            }

            Process = new EntityStepProcess(ClimbResults, ShiftResult, FallResults);
            return null;
        }

        private void CollectCostInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(CostInterrupts != null);

            CostInterrupts = ValueList<Interrupt<EntityStepCostEffect>>.Empty;
            var effectors = Component.GetAllOfTypeFrom<IEntityStepCostEffector>(History.World).Sort();
            foreach (var effector in effectors)
            {
                var interrupts = effector.CostEffectsOnEntityStep(History, Action, Process);
                // TODO: CostInterrupts を PriorityQueue にした方がいい
                CostInterrupts = CostInterrupts.AddRange(interrupts);
            }
        }

        [NotNull]
        private static EntityStepCost BaseCost([NotNull] EntityStepProcess process)
        {
            return ClimbCost * process.ClimbResults.Count + HorizontalCost + FallCost * process.FallResults.Count;
        }

        private void CalculateCost()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostInterrupts != null);
            Contract.Ensures(ExpendedCost != null);

            CostInterrupts = CostInterrupts.Sort();
            ExpendedCost = BaseCost(Process);
            foreach (var costInterrupt in CostInterrupts)
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
        }

        [CanBeNull]
        private EntityStepResult CheckCostIsEnough()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostInterrupts != null);
            Contract.Requires<InvalidOperationException>(ExpendedCost != null);

            if (Action.Available > ExpendedCost)
            {
                return new CostOverResult(Action, Process, CostInterrupts, ExpendedCost);
            }

            return null;
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostInterrupts != null);
            Contract.Requires<InvalidOperationException>(ExpendedCost != null);
            Contract.Ensures(Interrupts != null);

            Interrupts = ValueList<Interrupt<EntityStepEffect>>.Empty;
            var effectors = Component.GetAllOfTypeFrom<IEntityStepEffector>(History.World).Sort();
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnEntityStep(History, Action, Process, CostInterrupts, ExpendedCost);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private EntityStepResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostInterrupts != null);
            Contract.Requires<InvalidOperationException>(ExpendedCost != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(
                        Action,
                        Process,
                        CostInterrupts,
                        ExpendedCost,
                        Interrupts,
                        interrupt.ComponentID
                    );
                }
            }

            return null;
        }

        [NotNull]
        private EntityStepResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(CostInterrupts != null);
            Contract.Requires<InvalidOperationException>(ExpendedCost != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, CostInterrupts, ExpendedCost, Interrupts);
        }
    }
}
