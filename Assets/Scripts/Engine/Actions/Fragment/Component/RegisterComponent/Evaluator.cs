using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }

        [CanBeNull] private Component Registered { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = CheckParentExists();
            if (result != null) return result;

            Format();
            return Succeed();
        }

        [CanBeNull]
        private Result CheckParentExists()
        {
            if (Context.Finder.FindAttachable(Initial.World, Action.ParentID) == null)
            {
                return new ParentNotFoundResult(Action);
            }

            return null;
        }

        private void Format()
        {
            Registered = Action.InitialProps with
            {
                ID = Action.AssignedID,
                AttachedID = Action.ParentID,
            };
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Registered);
        }
    }
}
