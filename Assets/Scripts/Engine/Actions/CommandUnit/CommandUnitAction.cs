using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CommandUnit
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record CommandUnitAction() : Action(ActionType.CommandUnit)
    {
        [NotNull] public PlayerLocator Actor { get; init; } = new();
        [NotNull] public CommandApplication Application { get; init; } = new();

        public Event Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}