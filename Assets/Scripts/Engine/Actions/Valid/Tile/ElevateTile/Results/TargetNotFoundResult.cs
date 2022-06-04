using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record TargetNotFoundResult([NotNull] Action Action) : Result;
}
