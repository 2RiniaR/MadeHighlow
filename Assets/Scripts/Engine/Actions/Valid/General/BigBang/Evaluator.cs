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
        [NotNull] private Result Result { get; }

        [NotNull]
        public Result Evaluate()
        {
            JoinPlayers();
            GenerateTiles();
            GenerateEntities();
            WrapProcess();
            return Succeed();
        }

        private void JoinPlayers()
        {
            JoinPlayerEvents = ValueList<Event<ReactedResult<JoinPlayer.Result>>>.Empty;
            foreach (var player in Action.InitialWorld.Players)
            {
                var result = Context.Actions.JoinPlayer(Simulating, new JoinPlayer.Action(player));
                Simulating = Simulating.Appended(result, out var @event);
                JoinPlayerEvents = JoinPlayerEvents.Add(@event);
            }
        }

        private void GenerateTiles()
        {
            GenerateTileEvents = ValueList<Event<ReactedResult<GenerateTile.Result>>>.Empty;
            foreach (var tile in Action.InitialWorld.Tiles)
            {
                var result = Context.Actions.GenerateTile(Simulating, new GenerateTile.Action(tile));
                Simulating = Simulating.Appended(result, out var @event);
                GenerateTileEvents = GenerateTileEvents.Add(@event);
            }
        }

        private void GenerateEntities()
        {
            GenerateEntityEvents = ValueList<Event<ReactedResult<GenerateEntity.Result>>>.Empty;
            foreach (var entity in Action.InitialWorld.Entities)
            {
                var result = Context.Actions.GenerateEntity(Simulating, new GenerateEntity.Action(entity));
                Simulating = Simulating.Appended(result, out var @event);
                GenerateEntityEvents = GenerateEntityEvents.Add(@event);
            }
        }

        private void WrapProcess()
        {
            Process = new Process(JoinPlayerEvents, GenerateTileEvents, GenerateEntityEvents);
        }

        [NotNull]
        private Result Succeed()
        {
            return new Result(Action, Process);
        }
    }
}
