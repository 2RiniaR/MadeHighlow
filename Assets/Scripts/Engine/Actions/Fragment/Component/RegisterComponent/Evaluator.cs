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
            return Context.Finder.FindAttachable(Initial.World, Action.ParentID) != null;
        }

        private void Confirm()
        {
            var registered = Action.InitialProps with
            {
                ID = Action.AssignedID,
                AttachedID = Action.ParentID,
            };
            Result = Result with { Registered = registered };
        }
    }
}
