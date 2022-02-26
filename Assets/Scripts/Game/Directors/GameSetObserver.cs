using System.Linq;
using Game.Entities;

namespace Game.Directors
{
    /// <summary>
    /// ゲームの終了を判定するクラス
    /// </summary>
    public class GameSetObserver
    {
        private readonly IGameSession _session;

        public GameSetObserver(IGameSession session)
        {
            _session = session;
        }

        public bool IsGameSet()
        {
            return _session.Players.Count(IsPlayerDroppedOut) <= 1;
        }

        /// <summary>
        /// プレイヤーが脱落したかどうかを返す。
        /// </summary>
        private static bool IsPlayerDroppedOut(IPlayer player)
        {
            return player.Units.All(unit => !unit.IsLiving());
        }
    }
}