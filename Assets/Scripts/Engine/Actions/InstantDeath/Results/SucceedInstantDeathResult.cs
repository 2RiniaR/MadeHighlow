using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     即死効果を与えた結果
    /// </summary>
    public record SucceedInstantDeathResult(ID SourceID, [NotNull] EntityID TargetEntityID) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            var entity = TargetEntityID.GetFrom(world) ?? throw new NullReferenceException();
            var vitality = entity.Vitality ?? throw new NullReferenceException();

            var modifiedEntity = entity with { Vitality = vitality.Dead };
            return modifiedEntity.UpdateIn(world);
        }
    }
}