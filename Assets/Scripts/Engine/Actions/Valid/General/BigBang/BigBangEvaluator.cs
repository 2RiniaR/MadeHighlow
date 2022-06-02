using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity;
using RineaR.MadeHighlow.Actions.GenerateTile;
using RineaR.MadeHighlow.Actions.JoinPlayer;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public class BigBangEvaluator
    {
        public BigBangEvaluator([NotNull] EvaluationContext context, [NotNull] IHistory initial, BigBangAction action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private EvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private BigBangAction Action { get; }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<ReactedResult<JoinPlayerResult>>> JoinPlayerEvents { get; set; }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<ReactedResult<GenerateTileResult>>> GenerateTileEvents { get; set; }

        [CanBeNull]
        [ItemNotNull]
        private ValueList<Event<ReactedResult<GenerateEntityResult>>> GenerateEntityEvents { get; set; }

        [CanBeNull] private BigBangProcess Process { get; set; }

        [NotNull]
        public BigBangResult Evaluate()
        {
            JoinPlayers();
            GenerateTiles();
            GenerateEntities();
            WrapProcess();
            return Succeed();
        }

        private void JoinPlayers()
        {
            JoinPlayerEvents = ValueList<Event<ReactedResult<JoinPlayerResult>>>.Empty;
            foreach (var player in Action.InitialWorld.Players)
            {
                var result = Context.Actions.JoinPlayer(Simulating, new JoinPlayerAction(player));
                Simulating = Simulating.Appended(result, out var @event);
                JoinPlayerEvents = JoinPlayerEvents.Add(@event);
            }
        }

        private void GenerateTiles()
        {
            GenerateTileEvents = ValueList<Event<ReactedResult<GenerateTileResult>>>.Empty;
            foreach (var tile in Action.InitialWorld.Tiles)
            {
                var result = Context.Actions.GenerateTile(Simulating, new GenerateTileAction(tile));
                Simulating = Simulating.Appended(result, out var @event);
                GenerateTileEvents = GenerateTileEvents.Add(@event);
            }
        }

        private void GenerateEntities()
        {
            GenerateEntityEvents = ValueList<Event<ReactedResult<GenerateEntityResult>>>.Empty;
            foreach (var entity in Action.InitialWorld.Entities)
            {
                var result = Context.Actions.GenerateEntity(Simulating, new GenerateEntityAction(entity));
                Simulating = Simulating.Appended(result, out var @event);
                GenerateEntityEvents = GenerateEntityEvents.Add(@event);
            }
        }

        private void WrapProcess()
        {
            Process = new BigBangProcess(JoinPlayerEvents, GenerateTileEvents, GenerateEntityEvents);
        }

        [NotNull]
        private BigBangResult Succeed()
        {
            return new BigBangResult(Action, Process);
        }
    }
}
