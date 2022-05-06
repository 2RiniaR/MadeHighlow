using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行する際の、行動順を決定するクエリ
    /// </summary>
    public record StartCommandsOrderer([NotNull] [ItemNotNull] in ValueObjectList<Command> Commands)
    {
        /// <summary>
        ///     命令の実行順を決定する
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ValueObjectList<Command> Resolve([NotNull] IActionContext context)
        {
            return Commands.Sort((unit1, unit2) => Compare(unit1, unit2, context));
        }

        /// <summary>
        ///     2つのユニットの行動優先度を比較する。
        /// </summary>
        /// <returns>
        ///     `operation1`の方が優先度が高ければ、正の値を返す。
        ///     `operation2`の方が優先度が高ければ、負の値を返す。
        ///     `operation1`と`operation2`の優先度が等しければ、0を返す。
        /// </returns>
        private int Compare(
            [NotNull] in Command operation1,
            [NotNull] in Command operation2,
            [NotNull] in IActionContext context
        )
        {
            var world = context.World;

            // (1) 「コマンドの早さ」が早い順に行動する。
            var quicknessCompare = CompareCommandQuickness(operation1, operation2, world);
            if (quicknessCompare != 0)
            {
                return quicknessCompare;
            }

            var unit1 = operation1.UnitID.GetFrom(world);
            var unit2 = operation2.UnitID.GetFrom(world);

            // (2) (1)が同一の場合、「行動するユニットのメド」が高い順に行動する。
            var medoCompare = CompareMedo(unit1, unit2);
            if (medoCompare != 0)
            {
                return medoCompare;
            }

            // (3) (2)が同一の場合、「行動するユニットの体力」が高い順に行動する。
            var healthCompare = CompareHealth(unit1, unit2);
            if (healthCompare != 0)
            {
                return healthCompare;
            }

            // (4) (3)が同一の場合、ユニットの行動順はランダムとなる。
            return CompareRandom(context);
        }

        private static int CompareCommandQuickness(
            [NotNull] in Command operation1,
            [NotNull] in Command operation2,
            [NotNull] in World world
        )
        {
            var card1 = operation1.CardID.GetFrom(world) ?? throw new NullReferenceException();
            var card2 = operation2.CardID.GetFrom(world) ?? throw new NullReferenceException();

            var quickness1 = card1.Quickness;
            var quickness2 = card2.Quickness;

            return quickness1.CompareTo(quickness2);
        }

        private static int CompareMedo([NotNull] in Unit unit1, [NotNull] in Unit unit2)
        {
            var medo1 = unit1.Medo;
            var medo2 = unit2.Medo;

            return medo1.Value.CompareTo(medo2.Value);
        }

        private static int CompareHealth([NotNull] in Unit unit1, [NotNull] in Unit unit2)
        {
            if (unit1.Vitality == null)
            {
                return unit2.Vitality == null ? 0 : -1;
            }

            if (unit2.Vitality == null)
            {
                return 1;
            }

            var health1 = unit1.Vitality.Health;
            var health2 = unit2.Vitality.Health;

            return health2.Value.CompareTo(health1.Value);
        }

        private static int CompareRandom([NotNull] in IActionContext context)
        {
            return context.GetRandom() > 1 / 2f ? 1 : 0;
        }
    }
}