using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     即死効果を与えた結果
    /// </summary>
    public sealed record CausedInstantDeathResult(
        ID SourceID,
        [NotNull] EntityID TargetEntityID,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathEffect>> Interrupts
    ) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            var entity = TargetEntityID.GetFrom(world) ??
                         throw new NullReferenceException("対象の `Entity` が存在しない場合、アクションの結果は `Failed` になっていなければいけない");
            return Modified(entity).UpdateIn(world);
        }

        [NotNull]
        private Entity Modified([NotNull] Entity original)
        {
            var vitality = original.Vitality ?? throw new NullReferenceException(
                "対象の `Entity` の `Vitality` がnullの場合、アクションの結果は `Failed` になっていなければいけない"
            );
            return original with { Vitality = vitality.Dead };
        }
    }
}
