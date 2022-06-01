using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateTile;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public class GenerateTileEvaluator
    {
        public GenerateTileEvaluator(
            [NotNull] ActionContext context,
            [NotNull] IHistory initial,
            GenerateTileAction action
        )
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private ActionContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private GenerateTileAction Action { get; }

        [CanBeNull] private Event<CreateTile.SucceedResult> CreateTileEvent { get; set; }
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
            var result = Context.Actions.CreateTile(Simulating, new CreateTileAction(Action.InitialProps));
            if (result is not CreateTile.SucceedResult succeedResult)
            {
                return new CreateTileFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateTileEvent = succeedEvent;
            return null;
        }

        private void WrapProcess()
        {
            Process = new GenerateTileProcess(CreateTileEvent);
        }

        [CanBeNull]
        private GenerateTileResult CheckRejection()
        {
            var effectors = Component.GetAllOfTypeFrom<IGenerateTileRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<GenerateTileRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.GenerateTileRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
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
            return new SucceedResult(Action, Process, RejectionInterrupts);
        }
    }
}
