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

        /// <summary>
        /// プレイヤーが選択した行動をもとに、ターンを更新する。
        /// </summary>
        public void Update()
        {
            ApplyPlayerActions();
            IncrementTurnCount();
        }

        /// <summary>
        /// プレイヤーが選択した行動を適用する。
        /// </summary>
        private void ApplyPlayerActions()
        {
            var actions = new ActionOrderResolver(_session).Resolve();
            foreach (var action in actions)
            {
                action.UsedCard.Effect(_session, action.ActorUnit);
            }
        }

        private void IncrementTurnCount()
        {
            _session.CurrentTurn++;
        }
    }
}