using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record CommandUnitAction : Action<CommandUnitResult>
    {
        public ID ActorID { get; init; } = ID.None;
        public ID TargetID { get; init; } = ID.None;
        [NotNull] public UnitOperation Operation { get; init; } = UnitOperation.Empty;


        [NotNull]
        public override CommandUnitResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}