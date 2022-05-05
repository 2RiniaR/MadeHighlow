using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedCommandUnitResult() : CommandUnitResult(CommandUnitResultCode.Succeed)
    {
        [NotNull] public UnitEnsuredID TargetID { get; init; } = new();
        [NotNull] public UnitOperation Operation { get; init; } = new();

        public override World Simulate(in World world)
        {
            var unit = TargetID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedUnit = unit with { CurrentOperation = Operation };
            return modifiedUnit.UpdateIn(world);
        }
    }
}