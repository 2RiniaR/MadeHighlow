using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IResult
    {
        [NotNull]
        World Simulate([NotNull] SimulationContext context, [NotNull] World world);
    }
}
