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
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            if (!IsParentExists()) return Result;

            Confirm();

            return Result;
        }

        private bool IsParentExists()
        {
            return Context.Finder.FindPlayer(Initial.World, Action.ParentID) != null;
        }

        private void Confirm()
        {
            var registered = Action.InitialProps with
            {
                ID = Action.AssignedID,
                Components = ValueList<Component>.Empty,
            };
            Result = Result with { Registered = registered };
        }
    }
}
