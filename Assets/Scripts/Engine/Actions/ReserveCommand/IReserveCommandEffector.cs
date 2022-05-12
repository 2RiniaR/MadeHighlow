using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    /// <summary>
    ///     プレイヤーによるユニットの命令が、許可されるかどうかをチェックする
    /// </summary>
    public interface IReserveCommandEffector
    {
        public ValueList<Interrupt<ReserveCommandEffect>> EffectsOnReserveCommand(
            [NotNull] IActionContext session,
            [NotNull] ReserveCommandAction action
        );
    }
}
