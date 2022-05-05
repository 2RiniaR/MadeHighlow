using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行するアクション
    /// </summary>
    public record ActuateUnitAction : Action<ActuateUnitResult>
    {
        /// <summary>
        ///     実行を命令するユニット
        /// </summary>
        [NotNull]
        public UnitEnsuredID TargetID { get; init; } = new();

        public override ActuateUnitResult Validate(in IActionContext context)
        {
            var world = context.CurrentWorld();

            // カードを消費する

            // アクションを実行する

            // 結果を返す

            throw new NotImplementedException();
        }
    }
}