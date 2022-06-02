using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public record NotFoundResult([NotNull] UnregisterEntityAction Action) : UnregisterEntityResult;
}
