using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Primitives;

namespace Game.Entities
{
    public interface IGameSession
    {
        public GameID ID { get; }
        public int CurrentTurn { get; set; }
        public IEnumerable<IPlayer> Players { get; set; }
        public IField Field { get; set; }

        public GlobalSetting Setting { get; }

        public UniTask Run(CancellationToken cancellationToken = new CancellationToken());
    }
}