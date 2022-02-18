using System.Collections.Generic;
using Game.Primitives;
using JetBrains.Annotations;

namespace Game.Entities
{
    public interface IPlayer
    {
        public PlayerID ID { get; }
        public IClient CurrentClient { get; set; }
        public IEnumerable<IUnit> Units { get; set; }
        public IEnumerable<ICard> Cards { get; set; }
        [CanBeNull] public IPlayerTurnAction NextTurnAction { get; set; }
    }
}