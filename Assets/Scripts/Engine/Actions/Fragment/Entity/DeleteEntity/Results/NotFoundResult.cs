using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record NotFoundResult([NotNull] DeleteEntityAction Action) : DeleteEntityResult;
}
