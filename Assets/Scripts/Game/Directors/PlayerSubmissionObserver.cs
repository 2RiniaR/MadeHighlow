using System.Collections;
using Cysharp.Threading.Tasks;
using Game.Entities;

namespace Game.Directors
{
    /// <summary>
    /// プレイヤーの行動確定を監視するクラス
    /// </summary>
    public class PlayerSubmissionObserver
    {
        private readonly IGameSession _session;

        public PlayerSubmissionObserver(IGameSession session)
        {
            _session = session;
        }

        public IEnumerator WaitPlayersSubmission()
        {
            var players = _session.Players;
            return UniTask.WhenAll(players.Select(async player =>
            {
                var action = await player.CurrentClient.SubmitAction();
                player.NextTurnAction = action;
            })).ToCoroutine();
        }
    }
}