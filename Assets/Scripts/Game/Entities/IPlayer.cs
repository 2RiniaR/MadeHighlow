using System.Collections.Generic;
using Game.Primitives;
using JetBrains.Annotations;

namespace Game.Entities
{
    public interface IPlayer
    {
        public PlayerID ID { get; }

        public IClient CurrentClient { get; set; }
        public IReadOnlyCollection<IUnit> Units { get; }
        public IReadOnlyCollection<ICard> Cards { get; }

        [CanBeNull] public IPlayerTurnAction NextTurnAction { get; set; }
    }
}