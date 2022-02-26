using System;
using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Primitives;

namespace Game.Directors
{
    /// <summary>
    /// プレイヤーの行動順を決定するクラス
    /// </summary>
    public class ActionOrderResolver
    {
        private readonly IGameSession _session;

        public ActionOrderResolver(IGameSession session)
        {
            _session = session;
        }

        /// <summary>
        /// プレイヤーの次の行動から、行動順を決定して返す。
        /// </summary>
        public IEnumerable<IPlayerTurnAction> Resolve()
        {
            var actions = _session.Players.Select(player => player.NextTurnAction).ToList();
            actions.Sort(CompareActionPrimary);
            return actions;
        }

        /// <summary>
        /// 2つのアクションの優先度を比較する。
        /// </summary>
        /// <returns>
        /// `action1`の方が優先度が高ければ、正の値を返す。
        /// `action2`の方が優先度が高ければ、負の値を返す。
        /// `action1`と`action2`の優先度が等しければ、0を返す。
        /// </returns>
        private static int CompareActionPrimary(IPlayerTurnAction action1, IPlayerTurnAction action2)
        {
            /*
             * 【行動の優先順位】
             * 1. 「チャーム」または「ブレイク」を行うユニットで、HPが低い順に行動
             * 2. 「移動」を行うユニットで、HPが低い順に行動
             * 3. 「移動」以外を行うユニットで、HPが低い順に行動
             */

            var typeOrder1 = GetTypeOrder(action1.Type);
            var typeOrder2 = GetTypeOrder(action2.Type);
            if (typeOrder1 != typeOrder2) return typeOrder1.CompareTo(typeOrder2);

            var healthOrder1 = GetHealthOrder(action1.ActorUnit.Health);
            var healthOrder2 = GetHealthOrder(action2.ActorUnit.Health);
            return healthOrder1.CompareTo(healthOrder2);
        }

        /// <summary>
        /// 行動の種別について、行動の順番が早くなるものほど大きい整数を返す。
        /// </summary>
        private static int GetTypeOrder(ActionType type)
        {
            return type switch
            {
                ActionType.Move => 1,
                ActionType.Evolve => 2,
                ActionType.Other => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        /// <summary>
        /// ユニットのHPについて、行動の順番が早くなるものほど大きい整数を返す。
        /// </summary>
        private static int GetHealthOrder(int health)
        {
            return -health;
        }
    }
}