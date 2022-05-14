using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.FragmentActions;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    public class RegisterPlayerEvaluator
    {
        public RegisterPlayerEvaluator([NotNull] IActionContext context, [NotNull] Player initialProps)
        {
            Context = context;
            InitialProps = initialProps;
        }

        [NotNull] private IActionContext Context { get; }
        [NotNull] private Player InitialProps { get; }

        [CanBeNull] private AllocateIDResult AllocateIDResult { get; set; }
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
            Contract.Ensures(AllocateIDResult != null);

            AllocateIDResult = new AllocateIDAction().Evaluate(Context);
            Registered = InitialProps with
            {
                ID = AllocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterPlayerResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDResult != null);
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new RegisterPlayerResult(AllocateIDResult, Registered);
        }
    }
}
