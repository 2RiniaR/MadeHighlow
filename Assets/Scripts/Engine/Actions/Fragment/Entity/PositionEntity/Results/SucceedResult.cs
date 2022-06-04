using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Entity Positioned) : Result;
}
