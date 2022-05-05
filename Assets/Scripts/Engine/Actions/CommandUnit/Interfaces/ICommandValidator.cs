using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーによるユニットの命令が、許可されるかどうかをチェックする
    /// </summary>
    public interface ICommandValidator
    {
        public CommandValidation ValidateCommand(
            [NotNull] in IActionContext session,
            [NotNull] in CommandUnitAction action
        );
    }
}