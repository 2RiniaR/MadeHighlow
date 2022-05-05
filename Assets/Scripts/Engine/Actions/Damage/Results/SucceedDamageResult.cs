using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えた結果
    /// </summary>
    public record SucceedDamageResult : DamageResult
    {
        /// <summary>
        ///     ダメージを与えたオブジェクトのID
        /// </summary>
        public ID SourceID { get; init; } = ID.None;

        /// <summary>
        ///     ダメージを受けたエンティティのID
        /// </summary>
        public EntityEnsuredID TargetID { get; init; } = new();

        /// <summary>
        ///     与えようとしたダメージ量
        /// </summary>
        public Damage Damage { get; init; } = new();

        /// <summary>
        ///     影響したダメージ軽減効果
        /// </summary>
        public ValueObjectList<DamageReduction> Reductions { get; init; } = ValueObjectList<DamageReduction>.Empty;

        public override World Simulate(in World world)
        {
            var entity = TargetID.GetFrom(world) ?? throw new NullReferenceException();
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
            return Reductions.Aggregate(Damage, (damage, reduction) => reduction.Caused(damage));
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