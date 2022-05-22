using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity;
using RineaR.MadeHighlow.Actions.GenerateTile;
using RineaR.MadeHighlow.Actions.JoinPlayer;

namespace RineaR.MadeHighlow.Actions.General.BigBang
{
    public class BigBangEvaluator
    {
        public BigBangEvaluator([NotNull] IHistory initial, BigBangAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

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
            Contract.Ensures(JoinPlayerEvents != null);

            JoinPlayerEvents = ValueList<Event<ReactedResult<JoinPlayerResult>>>.Empty;
            foreach (var player in Action.InitialWorld.Players)
            {
                var result = new JoinPlayerAction(player).Evaluate(Simulating);
                Simulating = Simulating.Appended(result, out var @event);
                JoinPlayerEvents = JoinPlayerEvents.Add(@event);
            }
        }

        private void GenerateTiles()
        {
            Contract.Ensures(GenerateTileEvents != null);

            GenerateTileEvents = ValueList<Event<ReactedResult<GenerateTileResult>>>.Empty;
            foreach (var tile in Action.InitialWorld.Tiles)
            {
                var result = new GenerateTileAction(tile).Evaluate(Simulating);
                Simulating = Simulating.Appended(result, out var @event);
                GenerateTileEvents = GenerateTileEvents.Add(@event);
            }
        }

        private void GenerateEntities()
        {
            Contract.Ensures(GenerateEntityEvents != null);

            GenerateEntityEvents = ValueList<Event<ReactedResult<GenerateEntityResult>>>.Empty;
            foreach (var entity in Action.InitialWorld.Entities)
            {
                var result = new GenerateEntityAction(entity).Evaluate(Simulating);
                Simulating = Simulating.Appended(result, out var @event);
                GenerateEntityEvents = GenerateEntityEvents.Add(@event);
            }
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(JoinPlayerEvents != null);
            Contract.Requires<InvalidOperationException>(GenerateTileEvents != null);
            Contract.Requires<InvalidOperationException>(GenerateEntityEvents != null);
            Contract.Ensures(Process != null);

            Process = new BigBangProcess(JoinPlayerEvents, GenerateTileEvents, GenerateEntityEvents);
        }

        [NotNull]
        private BigBangResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);

            return new BigBangResult(Action, Process);
        }
    }
}
