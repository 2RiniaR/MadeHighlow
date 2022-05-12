using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public record RunningProcess(
        [CanBeNull] RegisterTile.SucceedResult RegisterTile = null,
        [CanBeNull] [ItemNotNull] ValueList<AddComponent.SucceedResult> AddComponents = null,
        [CanBeNull] PositionTile.SucceedResult PositionTile = null
    )
    {
        public FailedProcess AsFailed => new(RegisterTile, AddComponents?.Cast<AddComponentResult>(), PositionTile);

        public SucceedProcess AsSucceed => new(
            RegisterTile ?? throw new NullReferenceException(),
            AddComponents ?? throw new NullReferenceException(),
            PositionTile ?? throw new NullReferenceException()
        );
    }
}
