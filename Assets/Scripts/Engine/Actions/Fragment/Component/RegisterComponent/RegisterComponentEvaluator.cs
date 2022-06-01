using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class RegisterComponentEvaluator
    {
        public RegisterComponentEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            RegisterComponentAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private RegisterComponentAction Action { get; }

        [CanBeNull] private Component Registered { get; set; }

        [NotNull]
        public RegisterComponentResult Evaluate()
        {
            RegisterComponentResult result;

            result = CheckParentExists();
            if (result != null) return result;

            Format();
            return Succeed();
        }

        [CanBeNull]
        private RegisterComponentResult CheckParentExists()
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
        private RegisterComponentResult Succeed()
        {
            return new SucceedResult(Action, Registered);
        }
    }
}
