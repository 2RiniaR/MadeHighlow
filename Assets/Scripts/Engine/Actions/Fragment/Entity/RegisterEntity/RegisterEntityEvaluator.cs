using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterEntity
{
    public class RegisterEntityEvaluator
    {
        public RegisterEntityEvaluator([NotNull] IHistory initial, RegisterEntityAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            Contract.Ensures(Registered != null);

            Registered = Action.InitialProps with
            {
                ID = Action.AssignedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new RegisterEntityResult(Action, Registered);
        }
    }
}
