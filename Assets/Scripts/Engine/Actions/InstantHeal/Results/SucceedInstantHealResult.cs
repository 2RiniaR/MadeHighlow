using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     治癒効果を与えた結果
    /// </summary>
    public record SucceedInstantHealResult : InstantHealResult
    {
        /// <summary>
        ///     治癒効果を与えたオブジェクトのID
        /// </summary>
        public ID SourceID { get; init; } = ID.None;

        /// <summary>
        ///     治癒効果を受けたエンティティのID
        /// </summary>
        public EntityID TargetID { get; init; } = new();

        /// <summary>
        ///     与えようとした治癒効果の量
        /// </summary>
        public Heal Heal { get; init; } = new();

        /// <summary>
        ///     影響した治癒効果軽減効果
        /// </summary>
        public ValueObjectList<HealReduction> Reductions { get; init; } = ValueObjectList<HealReduction>.Empty;

        public override World Simulate(in World world)
        {
            var entity = TargetID.GetFrom(world) ?? throw new NullReferenceException();
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
            return Reductions.Aggregate(Heal, (damage, reduction) => reduction.Caused(damage));
        }

        /// <summary>
        ///     体力を変更したエンティティを取得する
        /// </summary>
        [NotNull]
        private Entity EntityModifiedWith([NotNull] in Entity original, [NotNull] in EntityHealth health)
        {
            var vitality = original.Vitality ?? throw new NullReferenceException();
            return original with
            {
                Vitality = vitality with { Health = health },
            };
        }
    }
}