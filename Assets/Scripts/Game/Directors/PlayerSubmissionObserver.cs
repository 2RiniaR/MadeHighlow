using System.Threading;
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

        public UniTask WaitPlayersSubmission(CancellationToken cancellationToken = new CancellationToken())
        {
            var players = _session.Players;
            return UniTask.WhenAll(players.Select(player => UpdateSubmission(player, cancellationToken)));
        }

        private static async UniTask UpdateSubmission(IPlayer player, CancellationToken cancellationToken = new CancellationToken())
        {
            var action = await player.CurrentClient.SubmitAction(cancellationToken);
            player.NextTurnAction = action;
        }
    }
}