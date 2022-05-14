using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.RegisterComponent
{
    public class RegisterComponentEvaluator
    {
        public RegisterComponentEvaluator(
            [NotNull] IHistory context,
            [NotNull] IAttachableID parentID,
            [NotNull] Component initialProps
        )
        {
            Context = context;
            ParentID = parentID;
            InitialProps = initialProps;
        }

        [NotNull] private IHistory Context { get; }
        [NotNull] private IAttachableID ParentID { get; }
        [NotNull] private Component InitialProps { get; }

        [CanBeNull] private AllocateIDResult AllocateIDResult { get; set; }
        [CanBeNull] private IAttachable Parent { get; set; }
        [CanBeNull] private Component Registered { get; set; }

        [NotNull]
        public RegisterComponentResult Evaluate()
        {
            RegisterComponentResult result;

            result = GetParent();
            if (result != null) return result;

            Format();
            return Succeed();
        }

        [CanBeNull]
        private RegisterComponentResult GetParent()
        {
            Contract.Ensures((Contract.Result<RegisterComponentResult>() != null) ^ (Parent != null));

            Parent = ParentID.GetFrom(Context.World);
            if (Parent == null)
            {
                return new ParentNotFoundResult(ParentID);
            }

            return null;
        }

        private void Format()
        {
            Contract.Requires<InvalidOperationException>(Parent != null);
            Contract.Ensures(Registered != null);
            Contract.Ensures(AllocateIDResult != null);

            AllocateIDResult = new AllocateIDAction().Evaluate(Context);
            Registered = InitialProps with
            {
                ID = AllocateIDResult.AllocatedID,
                AttachedID = ParentID,
            };
        }

        [NotNull]
        private RegisterComponentResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDResult != null);
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new SucceedResult(AllocateIDResult, Registered);
        }
    }
}
