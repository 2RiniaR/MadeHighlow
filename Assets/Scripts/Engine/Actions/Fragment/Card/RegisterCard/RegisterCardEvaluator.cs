using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterCard
{
    public class RegisterCardEvaluator
    {
        public RegisterCardEvaluator([NotNull] IHistory initial, RegisterCardAction action)
        {
            Initial = initial;
            Action = action;
        }

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
            if (Action.ParentID.GetFrom(Initial.World) == null)
            {
                return new ParentNotFoundResult(Action);
            }

            return null;
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
        private RegisterCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new SucceedResult(Action, Registered);
        }
    }
}
