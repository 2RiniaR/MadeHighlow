using System.Collections;
using System.Collections.Generic;
using Game.Primitives;

namespace Game.Entities
{
    public interface IGameSession
    {
        public GameID ID { get; }
        public IEnumerable<IPlayer> Players { get; set; }
        public IField Field { get; set; }

        public GlobalSetting Setting { get; }

        public IEnumerator Run();
    }
}