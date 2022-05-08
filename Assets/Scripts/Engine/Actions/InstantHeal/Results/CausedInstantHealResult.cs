using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒を与えた結果
    /// </summary>
    public sealed record CausedInstantHealResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Heal ExpectedHeal,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealEffect>> Interrupts,
        [NotNull] Heal ActualHeal
    ) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            var entity = TargetEntityID.GetFrom(world) ??
                         throw new NullReferenceException("対象の `Entity` が存在しない場合、アクションの結果は `Failed` になっていなければいけない");
            return Modified(entity).UpdateIn(world);
        }

        /// <summary>
        ///     体力を変更したエンティティを取得する
        /// </summary>
        [NotNull]
        private Entity Modified([NotNull] Entity original)
        {
            var vitality = original.Vitality ?? throw new NullReferenceException(
                "対象の `Entity` の `Vitality` がnullの場合、アクションの結果は `Failed` になっていなければいけない"
            );
            return original with
            {
                Vitality = vitality with { Health = ActualHeal.Caused(vitality.Health) },
            };
        }
    }
}
