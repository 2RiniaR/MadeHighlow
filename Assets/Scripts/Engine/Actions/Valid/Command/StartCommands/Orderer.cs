using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行する際の、行動順を決定するクエリ
    /// </summary>
    public class Orderer
    {
        public Orderer([NotNull] IEvaluationContext context)
        {
            Context = context;
        }

        [NotNull] private IEvaluationContext Context { get; }

        /// <summary>
        ///     命令の実行順を決定する
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ValueList<Command> Resolve(
            [NotNull] IHistory history,
            [NotNull] [ItemNotNull] ValueList<Command> commands
        )
        {
            return commands.Sort((unit1, unit2) => Compare(unit1, unit2, history));
        }

        /// <summary>
        ///     2つのユニットの行動優先度を比較する。
        /// </summary>
        /// <returns>
        ///     `command1`の方が優先度が高ければ、正の値を返す。
        ///     `command2`の方が優先度が高ければ、負の値を返す。
        ///     `command1`と`command2`の優先度が等しければ、0を返す。
        /// </returns>
        private int Compare([NotNull] Command command1, [NotNull] Command command2, [NotNull] IHistory history)
        {
            var world = history.World;

            // (1) 「コマンドの早さ」が早い順に行動する。
            var quicknessCompare = CompareQuickness(command1, command2, world);
            if (quicknessCompare != 0)
            {
                return quicknessCompare;
            }

            // ユニットが削除されるときは、コマンドも同時に削除されるはずなので、例外を投げる
            var unit1 = Context.Finder.FindUnit(world, command1.UnitID) ?? throw new NullReferenceException();
            var unit2 = Context.Finder.FindUnit(world, command2.UnitID) ?? throw new NullReferenceException();

            // (2) (1)が同一の場合、「行動するユニットのメド」が高い順に行動する。
            var medoCompare = CompareMedo(unit1, unit2);
            if (medoCompare != 0)
            {
                return medoCompare;
            }

            // (3) (2)が同一の場合、「行動するユニットの体力」が高い順に行動する。
            var healthCompare = CompareHealth(unit1, unit2, world);
            if (healthCompare != 0)
            {
                return healthCompare;
            }

            // (4) (3)が同一の場合、ユニットの行動順はランダムとなる。
            return CompareRandom();
        }

        private int CompareQuickness([NotNull] Command command1, [NotNull] Command command2, [NotNull] World world)
        {
            // カードが削除されるときは、コマンドも同時に削除されるはずなので、例外を投げる
            var card1 = Context.Finder.FindCard(world, command1.CardID) ?? throw new NullReferenceException();
            var card2 = Context.Finder.FindCard(world, command2.CardID) ?? throw new NullReferenceException();

            var quickness1 = card1.Quickness;
            var quickness2 = card2.Quickness;

            return quickness1.CompareTo(quickness2);
        }

        private int CompareMedo([NotNull] Unit unit1, [NotNull] Unit unit2)
        {
            var medo1 = unit1.Medo;
            var medo2 = unit2.Medo;

            return medo1.Value.CompareTo(medo2.Value);
        }

        private int CompareHealth([NotNull] Unit unit1, [NotNull] Unit unit2, [NotNull] World world)
        {
            var entity1 = Context.Finder.FindEntity(world, unit1.EntityID);
            var entity2 = Context.Finder.FindEntity(world, unit2.EntityID);
            if (entity1 == null) return entity2 == null ? 0 : -1;
            if (entity2 == null) return 1;

            if (entity1.Vitality == null) return entity2.Vitality == null ? 0 : -1;
            if (entity2.Vitality == null) return 1;

            var health1 = entity1.Vitality.Health;
            var health2 = entity2.Vitality.Health;

            return health2.Value.CompareTo(health1.Value);
        }

        private int CompareRandom()
        {
            return Context.RandomGenerator.GetRandom() > 1 / 2f ? 1 : 0;
        }
    }
}
