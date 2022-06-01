using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerEvaluator
    {
        public RegisterPlayerEvaluator(
            [NotNull] ActionContext context,
            [NotNull] IHistory initial,
            RegisterPlayerAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private ActionContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private RegisterPlayerAction Action { get; }

        [CanBeNull] private Player Registered { get; set; }

        [NotNull]
        public RegisterPlayerResult Evaluate()
        {
            Format();
            return Succeed();
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
        private RegisterPlayerResult Succeed()
        {
            return new RegisterPlayerResult(Action, Registered);
        }
    }
}
