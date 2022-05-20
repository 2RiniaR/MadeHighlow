using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public sealed record SucceedResult(
        [NotNull] InstantDeathAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathRejection>> RejectionInterrupts
    ) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new ArgumentException();
            var vitality = target.Vitality ?? throw new ArgumentException();
            var modifiedTarget = target with { Vitality = vitality.Dead };
            return modifiedTarget.UpdateIn(world);
        }
    }
}
