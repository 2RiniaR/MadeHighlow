using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PositionEntity;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public class MoveEntityEvaluator
    {
        public MoveEntityEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            MoveEntityAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private MoveEntityAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private ValueList<Interrupt<MoveEntityRejection>> RejectionInterrupts { get; set; }
        [CanBeNull] private Event<PositionEntity.SucceedResult> PositionEntityEvent { get; set; }
        [CanBeNull] private MoveEntityProcess Process { get; set; }

        [NotNull]
        public MoveEntityResult Evaluate()
        {
            MoveEntityResult result;

            result = FindTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private MoveEntityResult FindTarget()
        {
            Target = Context.Finder.FindEntity(Initial.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private MoveEntityResult Position()
        {
            var result = Context.Actions.PositionEntity(
                Simulating,
                new PositionEntityAction(Action.TargetID, Target.Position3D.MoveTo(Action.Direction, new Distance(1)))
            );
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
            Process = new MoveEntityProcess(PositionEntityEvent);
        }

        [CanBeNull]
        private MoveEntityResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IMoveEntityRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<MoveEntityRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.MoveEntityRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private MoveEntityResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
