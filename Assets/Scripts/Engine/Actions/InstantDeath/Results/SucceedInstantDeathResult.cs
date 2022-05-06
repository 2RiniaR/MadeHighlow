using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     即死効果を与えた結果
    /// </summary>
    public record SucceedInstantDeathResult : InstantDeathResult
    {
        /// <summary>
        ///     即死効果を与えたオブジェクトのID
        /// </summary>
        public ID SourceID { get; init; } = ID.None;

        /// <summary>
        ///     即死効果を受けたエンティティのID
        /// </summary>
        public EntityID TargetID { get; init; } = new();

        public override World Simulate(in World world)
        {
            var entity = TargetID.GetFrom(world) ?? throw new NullReferenceException();
            var vitality = entity.Vitality ?? throw new NullReferenceException();

            var modifiedEntity = entity with { Vitality = vitality.Dead };
            return modifiedEntity.UpdateIn(world);
        }
    }
}