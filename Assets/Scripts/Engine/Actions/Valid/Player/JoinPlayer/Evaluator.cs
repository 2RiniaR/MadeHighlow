using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = CreatePlayer();
            if (result != null) return result;

            WrapProcess();

            return Succeed();
        }

        [CanBeNull]
        private Result CreatePlayer()
        {
            var result = Context.Actions.CreatePlayer(Simulating, new CreatePlayer.Action(Action.InitialPlayer));
            if (result is not CreatePlayer.SucceedResult succeedResult)
            {
                return new CreatePlayerFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreatePlayerEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Process = new Process(CreatePlayerEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Process);
        }
    }
}
