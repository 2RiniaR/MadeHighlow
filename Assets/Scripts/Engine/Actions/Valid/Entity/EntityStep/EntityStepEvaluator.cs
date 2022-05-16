using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.MoveEntity;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public class EntityStepEvaluator
    {
        public EntityStepEvaluator(
            [NotNull] IHistory history,
            [NotNull] EntityID targetID,
            [NotNull] Direction2D direction,
            [NotNull] EntityStepCost available
        )
        {
            History = history;
            TargetID = targetID;
            Direction = direction;
            Available = available;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private EntityID TargetID { get; }
        [NotNull] private EntityStepCost Available { get; }
        [NotNull] private Direction2D Direction { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Entity SteppingTarget { get; set; }
        [CanBeNull] private GroundElevation DestinationElevation { get; set; }
        [CanBeNull] private ValueList<Fragment.MoveEntity.SucceedResult> ClimbResults { get; set; }
        [CanBeNull] private Fragment.MoveEntity.SucceedResult HorizontalResult { get; set; }
        [CanBeNull] private ValueList<Fragment.MoveEntity.SucceedResult> FallResults { get; set; }
        [CanBeNull] private ValueList<Interrupt<EntityStepEffect>> Interrupts { get; set; }

        [NotNull]
        public EntityStepResult Evaluate()
        {
            EntityStepResult result;

            result = GetTarget();
            if (result != null) return result;

            result = GetDestination();
            if (result != null) return result;

            result = MoveClimb();
            if (result != null) return result;

            result = MoveHorizontal();
            if (result != null) return result;

            result = MoveFall();
            if (result != null) return result;

            result = CollectInterrupts();
            if (result != null) return result;

            result = CheckCostIsEnough();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private EntityStepResult GetTarget()
        {
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new TargetNotFoundResult(TargetID);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult GetDestination()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (DestinationElevation != null));

            var position = Target.Position3D.To2D().MoveTo(Direction, new Distance(1));
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
        private EntityStepResult MoveClimb()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Ensures(ClimbResults != null);

            var distance = new Distance(DestinationElevation.Height.Value - Target.Position3D.Z.Value);
            ClimbResults = ValueList<Fragment.MoveEntity.SucceedResult>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(TargetID, Direction3D.ZPositive).Evaluate(History);
                if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
                {
                    return new ClimbMoveFailedResult();
                }

                ClimbResults = ClimbResults.Add(succeedResult);
                SteppingTarget = succeedResult.PositionEntityResult.Positioned;
                History = History.Appended(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult MoveHorizontal()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(ClimbResults != null);
            Contract.Ensures((Contract.Result<EntityStepResult>() != null) ^ (HorizontalResult != null));

            var result = new MoveEntityAction(TargetID, Direction.To3D).Evaluate(History);
            if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
            {
                return new HorizontalMoveFailedResult();
            }

            HorizontalResult = succeedResult;
            SteppingTarget = succeedResult.PositionEntityResult.Positioned;
            return null;
        }

        [CanBeNull]
        private EntityStepResult MoveFall()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(SteppingTarget != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Requires<InvalidOperationException>(ClimbResults != null);
            Contract.Requires<InvalidOperationException>(HorizontalResult != null);

            var distance = new Distance(DestinationElevation.Height.Value - SteppingTarget.Position3D.Z.Value);
            FallResults = ValueList<Fragment.MoveEntity.SucceedResult>.Empty;

            for (var i = 0; i < distance.Value; i++)
            {
                var result = new MoveEntityAction(TargetID, Direction3D.ZNegative).Evaluate(History);
                if (result is not Fragment.MoveEntity.SucceedResult succeedResult)
                {
                    return new FallMoveFailedResult();
                }

                FallResults = FallResults.Add(succeedResult);
                SteppingTarget = succeedResult.PositionEntityResult.Positioned;
                History = History.Appended(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Requires<InvalidOperationException>(SteppingTarget != null);
            Contract.Requires<InvalidOperationException>(DestinationElevation != null);
            Contract.Requires<InvalidOperationException>(ClimbResults != null);
            Contract.Requires<InvalidOperationException>(HorizontalResult != null);
            Contract.Requires<InvalidOperationException>(FallResults != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IEntityStepEffector>(History.World);
            Interrupts = effectors.SelectMany(
                    effector => effector.EffectsOnEntityStep(
                        History,
                        SteppingTarget,
                        ClimbResults,
                        HorizontalResult,
                        FallResults
                    )
                )
                .Sort();
            foreach (var interrupt in Interrupts)
            {
            }

            return null;
        }

        [CanBeNull]
        private EntityStepResult CheckCostIsEnough()
        {
            throw new NotImplementedException();
        }

        [NotNull]
        private EntityStepResult Succeed()
        {
            throw new NotImplementedException();
        }
    }
}
