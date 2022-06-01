using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterTile
{
    public class RegisterTileEvaluator
    {
        public RegisterTileEvaluator(
            [NotNull] EvaluationContext context,
            [NotNull] IHistory initial,
            RegisterTileAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private RegisterTileAction Action { get; }

        [CanBeNull] private Tile Registered { get; set; }

        [NotNull]
        public RegisterTileResult Evaluate()
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
        private RegisterTileResult Succeed()
        {
            return new RegisterTileResult(Action, Registered);
        }
    }
}
