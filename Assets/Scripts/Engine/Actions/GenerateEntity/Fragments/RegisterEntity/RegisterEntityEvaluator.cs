using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.FragmentActions;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity
{
    public class RegisterEntityEvaluator
    {
        public RegisterEntityEvaluator([NotNull] IActionContext context, [NotNull] Entity initialProps)
        {
            Context = context;
            InitialProps = initialProps;
        }

        [NotNull] private IActionContext Context { get; }
        [NotNull] private Entity InitialProps { get; }

        [CanBeNull] private AllocateIDResult AllocateIDResult { get; set; }
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
            Contract.Ensures(AllocateIDResult != null);

            AllocateIDResult = new AllocateIDAction().Evaluate(Context);
            Registered = InitialProps with
            {
                ID = AllocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDResult != null);
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new RegisterEntityResult(AllocateIDResult, Registered);
        }
    }
}
