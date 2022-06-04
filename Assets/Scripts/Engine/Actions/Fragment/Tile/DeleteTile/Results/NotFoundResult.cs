using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record NotFoundResult([NotNull] Action Action) : Result;
}
