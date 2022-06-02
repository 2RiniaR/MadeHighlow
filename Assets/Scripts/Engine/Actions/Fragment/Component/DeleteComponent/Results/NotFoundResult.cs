using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record NotFoundResult([NotNull] DeleteComponentAction Action) : DeleteComponentResult;
}
