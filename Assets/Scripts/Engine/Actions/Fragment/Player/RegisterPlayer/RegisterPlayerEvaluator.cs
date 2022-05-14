using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer
{
    public class RegisterPlayerEvaluator
    {
        public RegisterPlayerEvaluator([NotNull] IHistory history, [NotNull] Player initialProps)
        {
            History = history;
            InitialProps = initialProps;
        }

        [NotNull] private IHistory History { get; }
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

            AllocateIDResult = new AllocateIDAction().Evaluate(History);
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
