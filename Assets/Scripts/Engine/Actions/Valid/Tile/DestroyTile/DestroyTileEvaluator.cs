using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteTile;

namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public class DestroyTileEvaluator
    {
        public DestroyTileEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            DestroyTileAction action
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
        [NotNull] private DestroyTileAction Action { get; }

        [CanBeNull] private Event<DeleteTile.SucceedResult> DeleteTileEvent { get; set; }
        [CanBeNull] private DestroyTileProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<DestroyTileRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public DestroyTileResult Evaluate()
        {
            DestroyTileResult result;

            result = DeleteTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DestroyTileResult DeleteTarget()
        {
            var result = Context.Actions.DeleteTile(Simulating, new DeleteTileAction(Action.TargetID));
            if (result is not DeleteTile.SucceedResult succeedResult)
            {
                return new DeleteTileFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            DeleteTileEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new DestroyTileProcess(DeleteTileEvent);
        }

        [CanBeNull]
        private DestroyTileResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IDestroyTileRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DestroyTileRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.DestroyTileRejection(Simulating, Action, Process, RejectionInterrupts);
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
        private DestroyTileResult Succeed()
        {
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
