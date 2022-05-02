using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries;

namespace RineaR.MadeHighlow.Actions
{
    public record SucceedCommandUnitResult() : CommandUnitResult(CommandUnitResultCode.Succeed)
    {
        [NotNull] public EntityLocator Target { get; init; } = new();
        [CanBeNull] public UnitOperation Operation { get; init; } = null;

        public override World Simulate(in World world)
        {
            var unit = new GetUnitQuery { Locator = Target }.Run(world);
            return new UpdateEntityQuery
            {
                Locator = Target,
                Value = unit with { CurrentOperation = Operation },
            }.Run(world);
        }
    }
}