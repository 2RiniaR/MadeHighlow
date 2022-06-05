using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
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
            RunByOrder();
            return Result;
        }

        private void RunByOrder()
        {
            var commands = Initial.World.ReservedCommands;
            var orderedCommands = new Orderer(Context).Resolve(Simulating, commands);

            var events = ValueList<Event<ReactedResult<RunCommand.Result>>>.Empty;
            foreach (var command in orderedCommands)
            {
                var action = new RunCommand.Action(command);
                var result = Context.Actions.RunCommand(Initial, action);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            Result = Result with { RunCommand = events };
        }
    }
}
