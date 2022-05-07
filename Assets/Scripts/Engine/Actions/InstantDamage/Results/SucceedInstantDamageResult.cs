using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えた結果
    /// </summary>
    public record SucceedInstantDamageResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Damage InitialDamage,
        [NotNull] [ItemNotNull] ValueObjectList<DamageReduction> Reductions
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            var entity = TargetEntityID.GetFrom(world) ?? throw new NullReferenceException();
            var vitality = entity.Vitality ?? throw new NullReferenceException();
            var modifiedHealth = CalculatedDamage().Caused(vitality.Health);
            return EntityModifiedWith(entity, modifiedHealth).UpdateIn(world);
        }

        /// <summary>
        ///     計算したダメージを取得する
        /// </summary>
        [NotNull]
        private Damage CalculatedDamage()
        {
            return Reductions.Aggregate(InitialDamage, (damage, reduction) => reduction.Caused(damage));
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