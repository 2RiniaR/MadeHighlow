using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record CreateEntityFailedResult([NotNull] Action Action, [NotNull] CreateEntity.Result Failed) : Result;
}
