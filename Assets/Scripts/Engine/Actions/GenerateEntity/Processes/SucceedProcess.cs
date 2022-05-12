using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record SucceedProcess(
        [NotNull] RegisterEntity.SucceedResult RegisterEntity,
        [NotNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponents,
        [NotNull] PositionEntity.SucceedResult PositionEntity
    );
}
