using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public sealed record SucceedResult(
        ID SourceID,
        [NotNull] Entity Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathEffect>> Interrupts
    ) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return Modified(Target).UpdateIn(world);
        }

        /// <summary>
        ///     体力を変更したエンティティを取得する
        /// </summary>
        [NotNull]
        private Entity Modified([NotNull] Entity original)
        {
            var vitality = original.Vitality ??
                           throw new NullReferenceException(
                               "対象の `Entity` の `Vitality` がnullの場合、アクションの結果は `Failed` になっていなければいけない"
                           );
            return original with
            {
                Vitality = vitality.Dead,
            };
        }
    }
}
