using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
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
            JoinPlayers();
            GenerateTiles();
            GenerateEntities();
            return Result;
        }

        private void JoinPlayers()
        {
            var events = ValueList<Event<ReactedResult<JoinPlayer.Result>>>.Empty;
            foreach (var player in Action.InitialWorld.Players)
            {
                var action = new JoinPlayer.Action(player);
                var result = Context.Actions.JoinPlayer(Initial, action);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            Result = Result with { JoinPlayers = events };
        }

        private void GenerateTiles()
        {
            var events = ValueList<Event<ReactedResult<GenerateTile.Result>>>.Empty;
            foreach (var tile in Action.InitialWorld.Tiles)
            {
                var action = new GenerateTile.Action(tile);
                var result = Context.Actions.GenerateTile(Initial, action);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            Result = Result with { GenerateTiles = events };
        }

        private void GenerateEntities()
        {
            var events = ValueList<Event<ReactedResult<GenerateEntity.Result>>>.Empty;
            foreach (var entity in Action.InitialWorld.Entities)
            {
                var action = new GenerateEntity.Action(entity);
                var result = Context.Actions.GenerateEntity(Initial, action);
                Simulating = Simulating.Appended(result, out var @event);
                events = events.Add(@event);
            }

            Result = Result with { GenerateEntities = events };
        }
    }
}
