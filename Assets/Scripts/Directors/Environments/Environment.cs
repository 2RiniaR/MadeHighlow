using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Directors.Environments
{
    public record Environment([NotNull] IRandomGenerator RandomGenerator);
}
