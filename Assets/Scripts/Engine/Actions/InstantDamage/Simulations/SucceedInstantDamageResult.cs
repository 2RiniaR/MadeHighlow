using System;
using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えた結果
    /// </summary>
    public record SucceedInstantDamageResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] Damage Damage,
        [NotNull] [ItemNotNull] ValueObjectList<InstantDamageInterrupt> EffectEmissions
    ) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            if (IsRefused())
            {
                return world;
            }

            // 対象のエンティティが存在しなければ Failed になっているはずなので、例外を投げる
            var entity = TargetEntityID.GetFrom(world) ?? throw new NullReferenceException();

            // 対象のエンティティが声明を持っていなければ Failed になっているはずなので、例外を投げる
            var vitality = entity.Vitality ?? throw new NullReferenceException();

            var modifiedHealth = CalculatedDamage().Caused(vitality.Health);
            return EntityModifiedWith(entity, modifiedHealth).UpdateIn(world);
        }

        private bool IsRefused()
        {
            return EffectEmissions.Select(emissions => emissions.Effect.Refused)
                .RemoveNull()
                .Any(refused => refused.Value);
        }

        /// <summary>
        ///     計算したダメージを取得する
        /// </summary>
        [NotNull]
        private Damage CalculatedDamage()
        {
            return EffectEmissions.Select(emissions => emissions.Effect.Reduction)
                .RemoveNull()
                .Sort()
                .Aggregate(Damage, (damage, reduction) => reduction.Value.Caused(damage));
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
