using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行するアクション
    /// </summary>
    public record ActuateUnitAction : IValidatable
    {
        /// <summary>
        ///     実行を命令するユニット
        /// </summary>
        [NotNull]
        public ValueObjectList<UnitEnsuredID> TargetsID { get; init; } = ValueObjectList<UnitEnsuredID>.Empty;

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public ActuateUnitResult Validate([NotNull] in IActionContext context)
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