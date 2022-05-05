using System;
using System.Collections.Immutable;
using RineaR.MadeHighlow.Directors.Environments;

namespace RineaR.MadeHighlow.Directors.Orders
{
    /// <summary>
    ///     プレイヤーの行動順を決定するクラス
    /// </summary>
    public class CommandOrderResolver
    {
        public CommandOrderResolver(IRandomGenerator randomGenerator)
        {
            RandomGenerator = randomGenerator;
        }

        private IRandomGenerator RandomGenerator { get; }

        public ImmutableList<CommandApplication> Resolve(ImmutableList<CommandApplication> commands)
        {
            return commands.Sort(Compare).ToImmutableList();
        }

        /// <summary>
        ///     2つの命令の優先度を比較する。
        /// </summary>
        /// <returns>
        ///     `action1`の方が優先度が高ければ、正の値を返す。
        ///     `action2`の方が優先度が高ければ、負の値を返す。
        ///     `action1`と`action2`の優先度が等しければ、0を返す。
        /// </returns>
        private int Compare(CommandApplication command1, CommandApplication command2)
        {
            throw new NotImplementedException();

            // var card1 = Session.Current.Cards.Find(command1.Operation.CardID);
            // var card2 = Session.Current.Cards.Find(command2.Operation.CardID);
            //
            // var quicknessCompare = Compare(card1.Payload.Command.Quickness, card2.Payload.Command.Quickness);
            // if (quicknessCompare != 0) return quicknessCompare;
            //
            // var unit1 = Session.Current.Units.Find(command1.Target);
            // var unit2 = Session.Current.Units.Find(command2.Target);
            //
            // var medoCompare = Compare(unit1.Payload.Medo, unit2.Payload.Medo);
            // if (medoCompare != 0) return medoCompare;
            //
            // var healthCompare = Compare(unit1.Payload.Health, unit2.Payload.Health);
            // if (healthCompare != 0) return healthCompare;
            //
            // return RandomGenerator.CompareRandom();
        }

        private static int Compare(CommandQuickness quickness1, CommandQuickness quickness2)
        {
            return quickness1.CompareTo(quickness2);
        }

        private static int Compare(UnitMedo medo1, UnitMedo medo2)
        {
            return medo1.Value.CompareTo(medo2.Value);
        }

        private static int Compare(UnitHealth health1, UnitHealth health2)
        {
            return health2.Value.CompareTo(health1.Value);
        }
    }
}