using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Entities;

namespace Game.Directors
{
    /// <summary>
    /// ゲームの一連の流れを進行する
    /// </summary>
    public class RootDirector
    {
        private readonly IGameSession _session;

        public RootDirector(IGameSession session)
        {
            _session = session;
        }

        /// <summary>
        /// ゲームを終了まで実行するコルーチン
        /// </summary>
        public async UniTask Run(CancellationToken cancellationToken = new CancellationToken())
        {
            new StateInitializer(_session).Initialize();

            while (true)
            {
                await new PlayerSubmissionObserver(_session).WaitPlayersSubmission(cancellationToken);
                new TurnUpdater(_session).Update();

                if (new GameSetObserver(_session).IsGameSet())
                {
                    break;
                }
            }

            new ResultJudge(_session).Judge();
        }
    }
}