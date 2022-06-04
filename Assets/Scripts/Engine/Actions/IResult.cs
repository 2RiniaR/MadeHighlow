using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IResult
    {
        [NotNull]
        World Simulate([NotNull] ISimulationContext context, [NotNull] World world);
    }
}
