using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record CommandUnitAction : IValidatable
    {
        public ID ActorID { get; init; } = ID.None;
        public ID TargetID { get; init; } = ID.None;
        [NotNull] public UnitOperation Operation { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public CommandUnitResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}