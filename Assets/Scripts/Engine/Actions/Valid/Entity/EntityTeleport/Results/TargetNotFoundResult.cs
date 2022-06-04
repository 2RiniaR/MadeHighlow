using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record TargetNotFoundResult([NotNull] Action Action) : Result;
}
