using System.Collections.Generic;
using System.Linq;
using Game.Entities;

namespace Game.Directors
{
    public class UnitFinder
    {
        private readonly IGameSession _session;

        public UnitFinder(IGameSession session)
        {
            _session = session;
        }

        public IEnumerable<IUnit> All => _session.Players.SelectMany(player => player.Units);
    }
}