using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class CommandOrderer
    {
        /// <summary>
        ///     命令の実行順を決定する
        /// </summary>
        public ReadOnlyCollection<Command> Resolve(IEnumerable<Command> commands)
        {
            var commandsList = new List<Command>(commands);
            commandsList.Sort(Compare);
            return new ReadOnlyCollection<Command>(commandsList);
        }

        /// <summary>
        ///     2つのコマンドの実行優先度を比較する。
        /// </summary>
        /// <returns>
        ///     `command1`の方が優先度が高ければ、正の値を返す。
        ///     `command2`の方が優先度が高ければ、負の値を返す。
        ///     `command1`と`command2`の優先度が等しければ、0を返す。
        /// </returns>
        private int Compare(Command command1, Command command2)
        {
            // (1) 「コマンドの早さ」が早い順に行動する。
            var quicknessCompare = CompareQuickness(command1, command2);
            if (quicknessCompare != 0)
            {
                return quicknessCompare;
            }

            // (2) (1)が同一の場合、「行動するフィギュアのメド」が高い順に行動する。
            var medoCompare = CompareMedo(command1, command2);
            if (medoCompare != 0)
            {
                return medoCompare;
            }

            // (3) (2)が同一の場合、「行動するフィギュアの体力」が高い順に行動する。
            var healthCompare = CompareHealth(command1, command2);
            if (healthCompare != 0)
            {
                return healthCompare;
            }

            // (4) (3)が同一の場合、コマンドの実行順はランダムとなる。
            return CompareRandom();
        }

        private int CompareQuickness(Command command1, Command command2)
        {
            var quickness1 = command1.quickness;
            var quickness2 = command2.quickness;
            return quickness1.priority.CompareTo(quickness2.priority);
        }

        private int CompareMedo(Command command1, Command command2)
        {
            var medo1 = command1.figure.medo;
            var medo2 = command2.figure.medo;
            return medo1.CompareTo(medo2);
        }

        private int CompareHealth(Command command1, Command command2)
        {
            var health1 = command1.figure != null ? command1.figure.life.health : 0;
            var health2 = command2.figure != null ? command2.figure.life.health : 0;
            return health2.CompareTo(health1);
        }

        private int CompareRandom()
        {
            return Random.Range(0f, 1f) > 0.5f ? 1 : 0;
        }
    }
}