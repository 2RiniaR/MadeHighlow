using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record CreateEntityFailedResult(
        [NotNull] GenerateEntityAction Action,
        [NotNull] CreateEntityResult Failed
    ) : GenerateEntityResult;
}
