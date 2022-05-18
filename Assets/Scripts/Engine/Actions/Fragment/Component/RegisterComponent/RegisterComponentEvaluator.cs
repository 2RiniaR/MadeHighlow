using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterComponent
{
    public class RegisterComponentEvaluator
    {
        public RegisterComponentEvaluator([NotNull] IHistory initial, RegisterComponentAction action)
        {
            Initial = initial;
            Action = action;
        }

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
                AttachedID = Action.ParentID,
            };
        }

        [NotNull]
        private RegisterComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new SucceedResult(Action, Registered);
        }
    }
}
