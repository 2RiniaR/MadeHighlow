using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface ISimulationContext
    {
        [NotNull] IWorldFinder Finder { get; }
        [NotNull] IWorldModifier Modifier { get; }
    }
}
