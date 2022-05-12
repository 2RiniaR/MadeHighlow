using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record RunningProcess(
        [CanBeNull] RegisterEntity.SucceedResult RegisterEntity = null,
        [CanBeNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponents = null,
        [CanBeNull] PositionEntity.SucceedResult PositionEntity = null
    )
    {
        public FailedProcess AsFailed => new(RegisterEntity, AddComponents?.Cast<AddComponentResult>(), PositionEntity);

        public SucceedProcess AsSucceed => new(
            RegisterEntity ?? throw new NullReferenceException(),
            AddComponents ?? throw new NullReferenceException(),
            PositionEntity ?? throw new NullReferenceException()
        );
    }
}
