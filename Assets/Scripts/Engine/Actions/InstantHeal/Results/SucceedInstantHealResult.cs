using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果を与えた結果
    /// </summary>
    public record SucceedInstantHealResult(
        in ID SourceID,
        [NotNull] in EntityID TargetEntityID,
        [NotNull] in Heal InitialHeal,
        [NotNull] [ItemNotNull] in ValueObjectList<HealReduction> Reductions
    ) : InstantHealResult
    {
        public override World Simulate(in World world)
        {
            var entity = TargetEntityID.GetFrom(world) ?? throw new NullReferenceException();
            var vitality = entity.Vitality ?? throw new NullReferenceException();
            var modifiedHealth = CalculatedHeal().Caused(vitality.Health);
            return EntityModifiedWith(entity, modifiedHealth).UpdateIn(world);
        }

        /// <summary>
        ///     計算した治癒効果を取得する
        /// </summary>
        [NotNull]
        private Heal CalculatedHeal()
        {
            return Reductions.Aggregate(InitialHeal, (damage, reduction) => reduction.Caused(damage));
        }

        /// <summary>
        ///     体力を変更したエンティティを取得する
        /// </summary>
        [NotNull]
        private Entity EntityModifiedWith([NotNull] in Entity original, [NotNull] in Health health)
        {
            var vitality = original.Vitality ?? throw new NullReferenceException();
            return original with
            {
                Vitality = vitality with { Health = health },
            };
        }
    }
}