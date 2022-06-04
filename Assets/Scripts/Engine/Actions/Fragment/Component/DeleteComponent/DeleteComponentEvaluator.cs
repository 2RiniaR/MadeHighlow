using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public class DeleteComponentEvaluator
    {
        public DeleteComponentEvaluator(
            [NotNull] IEvaluationContext context,
            [NotNull] IHistory initial,
            DeleteComponentAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private DeleteComponentAction Action { get; }

        [CanBeNull] private ValueList<Interrupt<DeleteComponentRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public DeleteComponentResult Evaluate()
        {
            DeleteComponentResult result;

            result = CheckTargetExist();
            if (result != null) return result;

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private DeleteComponentResult CheckTargetExist()
        {
            if (Context.Finder.FindComponent(Initial.World, Action.TargetID) == null)
            {
                return new NotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private DeleteComponentResult CheckRejection()
        {
            var effectors = Context.Finder.GetAllComponents<IDeleteComponentRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<DeleteComponentRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.DeleteComponentRejection(Initial, Action, RejectionInterrupts);
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
        private DeleteComponentResult Succeed()
        {
            return new SucceedResult(Action, RejectionInterrupts);
        }
    }
}
