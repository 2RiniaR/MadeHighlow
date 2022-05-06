using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record ReserveCommandAction([NotNull] in Command Command) : Action<ReserveCommandResult>
    {
        public override ReserveCommandResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}