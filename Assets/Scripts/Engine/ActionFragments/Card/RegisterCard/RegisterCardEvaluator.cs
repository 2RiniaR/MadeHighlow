using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterCard
{
    public class RegisterCardEvaluator
    {
        public RegisterCardEvaluator(
            [NotNull] IActionContext context,
            [NotNull] PlayerID parentID,
            [NotNull] Card initialProps
        )
        {
            Context = context;
            ParentID = parentID;
            InitialProps = initialProps;
        }

        [NotNull] private IActionContext Context { get; }
        [NotNull] private PlayerID ParentID { get; }
        [NotNull] private Card InitialProps { get; }

        [CanBeNull] private AllocateIDResult AllocateIDResult { get; set; }
        [CanBeNull] private Player Parent { get; set; }
        [CanBeNull] private Card Registered { get; set; }

        [NotNull]
        public RegisterCardResult Evaluate()
        {
            RegisterCardResult result;

            result = GetParent();
            if (result != null) return result;

            Format();
            return Succeed();
        }

        [CanBeNull]
        private RegisterCardResult GetParent()
        {
            Contract.Ensures((Contract.Result<RegisterCardResult>() != null) ^ (Parent != null));

            Parent = ParentID.GetFrom(Context.World);
            if (Parent == null)
            {
                return new ParentNotFoundResult(ParentID);
            }

            return null;
        }

        private void Format()
        {
            Contract.Ensures(Registered != null);
            Contract.Ensures(AllocateIDResult != null);

            AllocateIDResult = new AllocateIDAction().Evaluate(Context);
            Registered = InitialProps with
            {
                ID = AllocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDResult != null);
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new SucceedResult(AllocateIDResult, Registered);
        }
    }
}
