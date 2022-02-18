using System.Collections;

namespace Game.Directors
{
    /// <summary>
    /// ゲームの一連の流れを進行する
    /// </summary>
    public class RootDirector
    {
        private readonly PlayerSubmissionObserver _players;
        private readonly TurnUpdater _turn;
        private readonly GameSetObserver _gameSet;
        private readonly ResultJudge _result;

        public RootDirector(PlayerSubmissionObserver players, TurnUpdater turn, GameSetObserver gameSet, ResultJudge result)
        {
            _players = players;
            _turn = turn;
            _gameSet = gameSet;
            _result = result;
        }

        /// <summary>
        /// ゲームを終了まで実行するコルーチン
        /// </summary>
        public IEnumerator Run()
        {
            while (true)
            {
                yield return _players.WaitPlayersSubmission();
                _turn.Update();
                if (_gameSet.IsGameSet()) break;
            }

            _result.Judge();
        }
    }
}