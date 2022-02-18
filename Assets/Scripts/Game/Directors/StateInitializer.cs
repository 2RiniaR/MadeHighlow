using System.Collections.Generic;
using Game.Entities;

namespace Game.Directors
{
    /// <summary>
    /// ゲームの状態を初期化するクラス
    /// </summary>
    public class StateInitializer
    {
        private readonly IGameSession _session;

        public StateInitializer(IGameSession session)
        {
            _session = session;
        }

        public void InitializeGame(GlobalSetting globalSetting)
        {
            GeneratePlayers();
        }

        private void GeneratePlayers()
        {
        }
    }
}