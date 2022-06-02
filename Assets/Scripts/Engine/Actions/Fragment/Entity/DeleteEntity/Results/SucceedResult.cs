using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record SucceedResult(
        [NotNull] DeleteEntityAction Action,
        [NotNull] DeleteEntityProcess Process
    ) : DeleteEntityResult;
}
