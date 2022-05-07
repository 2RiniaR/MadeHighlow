using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーによるユニットの命令が、許可されるかどうかをチェックする
    /// </summary>
    public interface IReserveCommandValidator
    {
        public ReserveCommandValidation ValidateReserveCommand(
            [NotNull] IActionContext session,
            [NotNull] ReserveCommandAction action
        );
    }
}
