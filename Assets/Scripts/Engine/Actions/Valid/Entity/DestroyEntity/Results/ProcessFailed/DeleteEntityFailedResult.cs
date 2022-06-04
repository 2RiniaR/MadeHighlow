using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DeleteEntityFailedResult([NotNull] Action Action, [NotNull] DeleteEntity.Result Failed) : Result;
}
