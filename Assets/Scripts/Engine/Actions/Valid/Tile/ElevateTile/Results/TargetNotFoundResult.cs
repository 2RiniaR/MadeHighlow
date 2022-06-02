using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record TargetNotFoundResult([NotNull] ElevateTileAction Action) : ElevateTileResult;
}
