using Game.Entities;
using UnityEngine;

namespace Game.Directors
{
    /// <summary>
    /// ゲームの最終結果を判定するクラス
    /// </summary>
    public class ResultJudge
    {
        private readonly IGameSession _session;

        public ResultJudge(IGameSession session)
        {
            _session = session;
        }

        public void Judge()
        {
            Debug.Log("WHAT SHOULD I DO?");
        }
    }
}