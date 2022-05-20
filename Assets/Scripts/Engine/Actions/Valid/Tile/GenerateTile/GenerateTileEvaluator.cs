using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateTile;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public class GenerateTileEvaluator
    {
        public GenerateTileEvaluator([NotNull] IHistory initial, GenerateTileAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private GenerateTileAction Action { get; }

        [CanBeNull] private Event<Fragment.CreateTile.SucceedResult> CreateTileEvent { get; set; }
        [CanBeNull] private GenerateTileProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<GenerateTileRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public GenerateTileResult Evaluate()
        {
            GenerateTileResult result;

            result = CreateTarget();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private GenerateTileResult CreateTarget()
        {
            Contract.Ensures((Contract.Result<GenerateTileResult>() != null) ^ (CreateTileEvent != null));

            var result = new CreateTileAction(Action.InitialProps).Evaluate(Simulating);
            if (result is not Fragment.CreateTile.SucceedResult succeedResult)
            {
                return new CreateTileFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateTileEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(CreateTileEvent != null);
            Contract.Ensures(Process != null);

            Process = new GenerateTileProcess(CreateTileEvent);
        }

        [CanBeNull]
        private GenerateTileResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IGenerateTileRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<GenerateTileRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.GenerateTileRejection(Simulating, Action, Process, RejectionInterrupts);
                RejectionInterrupts = RejectionInterrupts.Add(interrupts);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(Action, Process, RejectionInterrupts, RejectionInterrupts[0].ComponentID);
            }

            return null;
        }

        [NotNull]
        private GenerateTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
