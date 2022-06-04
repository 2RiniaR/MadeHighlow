using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
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

        private Card Registered { get; set; }

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
            if (Context.Finder.FindPlayer(Initial.World, Action.ParentID) == null)
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
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Registered);
        }
    }
}
