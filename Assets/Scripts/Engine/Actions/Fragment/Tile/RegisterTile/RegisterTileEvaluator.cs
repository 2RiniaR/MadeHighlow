using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterTile
{
    public class RegisterTileEvaluator
    {
        public RegisterTileEvaluator([NotNull] IHistory history, [NotNull] Tile initialProps)
        {
            History = history;
            InitialProps = initialProps;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private Tile InitialProps { get; }

        [CanBeNull] private AllocateIDResult AllocateIDResult { get; set; }
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
            Contract.Ensures(AllocateIDResult != null);

            AllocateIDResult = new AllocateIDAction().Evaluate(History);
            Registered = InitialProps with
            {
                ID = AllocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
        }

        [NotNull]
        private RegisterTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(AllocateIDResult != null);
            Contract.Requires<InvalidOperationException>(Registered != null);

            return new RegisterTileResult(AllocateIDResult, Registered);
        }
    }
}
