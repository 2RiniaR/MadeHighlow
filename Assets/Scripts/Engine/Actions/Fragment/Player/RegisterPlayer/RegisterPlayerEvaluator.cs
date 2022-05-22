using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerEvaluator
    {
        public RegisterPlayerEvaluator([NotNull] IHistory initial, RegisterPlayerAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            Contract.Ensures(Registered != null);

            Registered = Action.InitialProps with
            {
                ID = Action.AssignedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterPlayerResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new RegisterPlayerResult(Action, Registered);
        }
    }
}
