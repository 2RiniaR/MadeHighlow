using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record NotFoundResult([NotNull] Action Action) : Result;
}
