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
        public ValueObjectList<UnitEnsuredID> TargetsID { get; init; } = ValueObjectList<UnitEnsuredID>.Empty;


        [NotNull]
        public override ActuateUnitResult Validate([NotNull] in IActionContext context)
        {
            var currentWorld = context.CurrentWorld();

            // 命令を実行する順番を解決する
            var orderedUnits = new ActuateUnitOrderer { TargetsID = TargetsID }.Resolve(currentWorld);

            // 順番に命令を実行する

            // カードを消費する

            // アクションを実行する

            // 結果を返す

            throw new NotImplementedException();
        }
    }
}