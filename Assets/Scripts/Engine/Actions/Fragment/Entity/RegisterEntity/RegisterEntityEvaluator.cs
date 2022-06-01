using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterEntity
{
    public class RegisterEntityEvaluator
    {
        public RegisterEntityEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            RegisterEntityAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private RegisterEntityAction Action { get; }

        [CanBeNull] private Entity Registered { get; set; }

        [NotNull]
        public RegisterEntityResult Evaluate()
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
        private RegisterEntityResult Succeed()
        {
            return new RegisterEntityResult(Action, Registered);
        }
    }
}
