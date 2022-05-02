using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record CommandUnitAction() : Action(ActionType.CommandUnit)
    {
        [NotNull] public PlayerLocator Actor { get; init; } = new();
        [NotNull] public EntityLocator Target { get; init; } = new();
        [NotNull] public UnitOperation Operation { get; init; } = new();

        public Result Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}