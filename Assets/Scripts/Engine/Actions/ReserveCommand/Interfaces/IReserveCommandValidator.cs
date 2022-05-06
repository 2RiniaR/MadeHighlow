using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーによるユニットの命令が、許可されるかどうかをチェックする
    /// </summary>
    public interface IReserveCommandValidator<TOption>
    {
        public ReserveCommandValidation ValidateReserveCommand(
            [NotNull] in IActionContext session,
            [NotNull] in ReserveCommandAction<TOption> action
        );
    }
}