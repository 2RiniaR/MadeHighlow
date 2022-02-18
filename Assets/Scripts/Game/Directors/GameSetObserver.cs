using System;
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
            throw new NotImplementedException();
        }
    }
}