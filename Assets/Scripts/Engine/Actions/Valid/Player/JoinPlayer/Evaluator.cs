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
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            CreatePlayer();
            return Result;
        }

        private void CreatePlayer()
        {
            var action = new CreatePlayer.Action(Action.InitialPlayer);
            var result = Context.Actions.CreatePlayer(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { CreatePlayer = @event };
        }
    }
}
