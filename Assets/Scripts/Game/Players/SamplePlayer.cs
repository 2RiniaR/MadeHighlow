using System.Collections.Generic;
using Game.Entities;
using Game.Primitives;

namespace Game.Players
{
    public class SamplePlayer : IPlayer
    {
        public PlayerID ID { get; }
        public IClient CurrentClient { get; set; }
        public IReadOnlyCollection<IUnit> Units { get; set; }
        public IReadOnlyCollection<ICard> Cards { get; set; }
        public IPlayerTurnAction NextTurnAction { get; set; }
    }
}