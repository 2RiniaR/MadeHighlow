using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterTile
{
    public class RegisterTileEvaluator
    {
        public RegisterTileEvaluator([NotNull] IHistory initial, RegisterTileAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            Contract.Ensures(Registered != null);

            Registered = Action.InitialProps with
            {
                ID = Action.AssignedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new RegisterTileResult(Action, Registered);
        }
    }
}
