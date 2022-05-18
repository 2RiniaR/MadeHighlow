using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public record SucceedResult(
        [NotNull] DeleteComponentAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DeleteComponentEffect>> Interrupts
    ) : DeleteComponentResult
    {
        public override World Simulate(World world)
        {
            var target = Action.TargetID.GetFrom(world) ?? throw new ArgumentException();
            return target.DeleteFrom(world);
        }
    }
}
