using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.GenerateEntity.PositionEntity;
using RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record FailedProcess(
        [CanBeNull] RegisterEntityResult RegisterEntity,
        [CanBeNull] [ItemNotNull] ValueList<AddComponentResult> AddComponents,
        [CanBeNull] PositionEntityResult PositionEntity
    );
}
