using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public class RegisterCardEvaluator
    {
        public RegisterCardEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            RegisterCardAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private RegisterCardAction Action { get; }

        [CanBeNull] private Card Registered { get; set; }

        [NotNull]
        public RegisterCardResult Evaluate()
        {
            RegisterCardResult result;

            result = CheckParentExists();
            if (result != null) return result;

            Format();
            return Succeed();
        }

        [CanBeNull]
        private RegisterCardResult CheckParentExists()
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
        private RegisterCardResult Succeed()
        {
            return new SucceedResult(Action, Registered);
        }
    }
}
