using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Directors;
using Game.Entities;
using Game.Primitives;

namespace Game.Sessions
{
    public class OfflineGameSession : IGameSession
    {
        public GameID ID { get; } = GameID.None;

        public int CurrentTurn { get; set; } = 0;
        public IEnumerable<IPlayer> Players { get; set; }
        public IField Field { get; set; }
        public GlobalSetting Setting { get; set; }

        private readonly RootDirector _director;

        public OfflineGameSession()
        {
            _director = new RootDirector(this);
        }

        public UniTask Run(CancellationToken cancellationToken = new CancellationToken())
        {
            return _director.Run(cancellationToken);
        }
    }
}