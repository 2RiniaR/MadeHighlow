using System.Collections;
using System.Collections.Generic;
using Game.Directors;
using Game.Entities;
using Game.Primitives;

namespace Game.Sessions
{
    public class OfflineGameSession : IGameSession
    {
        public GameID ID { get; } = GameID.None;
        public IEnumerable<IPlayer> Players { get; set; }
        public IField Field { get; set; }
        public GlobalSetting Setting { get; set; }

        private readonly RootDirector _director;

        public OfflineGameSession()
        {
            var players = new PlayerSubmissionObserver(this);
            var turn = new TurnUpdater(this);
            var gameSet = new GameSetObserver(this);
            var result = new ResultJudge(this);
            _director = new RootDirector(players, turn, gameSet, result);
        }

        public IEnumerator Run()
        {
            return _director.Run();
        }
    }
}