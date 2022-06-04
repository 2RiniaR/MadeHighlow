using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public class ElevateTileEvaluator
    {
        public ElevateTileEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            ElevateTileAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private ElevateTileAction Action { get; }

        [CanBeNull] private Tile Target { get; set; }

        [CanBeNull] private ValueList<Interrupt<ElevateTileRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public ElevateTileResult Evaluate()
        {
            ElevateTileResult result;

            result = FindTarget();
            if (result != null) return result;

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private ElevateTileResult FindTarget()
        {
            Target = Context.Finder.FindTile(Initial.World, Action.TargetID);
            if (Target == null)
            {
                return new TargetNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private ElevateTileResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IElevateTileRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<ElevateTileRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.ElevateTileRejection(Initial, Action, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private ElevateTileResult Succeed()
        {
            return new SucceedResult(Action, RejectionInterrupts);
        }
    }
}
