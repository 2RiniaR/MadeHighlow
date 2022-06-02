using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public record SucceedResult([NotNull] UnregisterEntityAction Action) : UnregisterEntityResult;
}
