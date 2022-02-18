using Game.Entities;

namespace Game.Directors
{
    /// <summary>
    /// ターンを更新するクラス
    /// </summary>
    public class TurnUpdater
    {
        private readonly IGameSession _session;

        public TurnUpdater(IGameSession session)
        {
            _session = session;
        }

        public void Update()
        {
            UpdateUnits();
        }

        private void UpdateUnits()
        {
            // ユニットの行動順を決める
            // 順番に、ユニットの行動を適用する
        }
    }
}