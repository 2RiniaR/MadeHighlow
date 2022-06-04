using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
