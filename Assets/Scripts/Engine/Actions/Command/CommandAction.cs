using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Actions.Command
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record CommandAction() : Action(ActionType.Command)
    {
        [NotNull] public PlayerLocator Actor { get; init; } = new();
        [NotNull] public CommandApplication Application { get; init; } = new();

        public override EventTimeline Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}