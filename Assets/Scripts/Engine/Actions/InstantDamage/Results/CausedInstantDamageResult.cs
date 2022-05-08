using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えた結果
    /// </summary>
    public sealed record CausedInstantDamageResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Damage ExpectedDamage,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageEffect>> Interrupts,
        [NotNull] Damage ActualDamage
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            var entity = TargetEntityID.GetFrom(world) ??
                         throw new NullReferenceException("対象のエンティティが存在しない場合、アクションの結果は `Failed` になっていなければいけない");
            var vitality = entity.Vitality ??
                           throw new NullReferenceException("対象のエンティティが生命を持っていない場合、アクションの結果は `Failed` になっていなければいけない");
            var modifiedHealth = ActualDamage.Caused(vitality.Health);
            return EntityModifiedWith(entity, modifiedHealth).UpdateIn(world);
        }

        /// <summary>
        ///     体力を変更したエンティティを取得する
        /// </summary>
        [NotNull]
        private Entity EntityModifiedWith([NotNull] Entity original, [NotNull] Health health)
        {
            var vitality = original.Vitality ?? throw new NullReferenceException();
            return original with
            {
                Vitality = vitality with { Health = health },
            };
        }
    }
}
