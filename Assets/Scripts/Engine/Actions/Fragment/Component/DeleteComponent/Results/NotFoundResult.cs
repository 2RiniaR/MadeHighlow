using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record NotFoundResult([NotNull] Action Action) : Result;
}
