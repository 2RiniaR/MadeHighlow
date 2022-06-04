using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record PositionEntityFailedResult([NotNull] Action Action, [NotNull] PositionEntity.Result Failed) : Result;
}
