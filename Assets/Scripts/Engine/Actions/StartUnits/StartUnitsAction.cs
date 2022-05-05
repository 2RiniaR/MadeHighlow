using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     複数のユニットが一斉に現在受けている命令を実行するアクション
    /// </summary>
    public record StartUnitsAction : Action<ActuateUnitResult>
    {
        /// <summary>
        ///     実行を命令するユニット
        /// </summary>
        [NotNull]
        public ValueObjectList<UnitEnsuredID> TargetsID { get; init; } = ValueObjectList<UnitEnsuredID>.Empty;

        public override ActuateUnitResult Validate(in IActionContext context)
        {
            var world = context.CurrentWorld();

            // 命令を実行する順番を解決する
            var operations = TargetsID
                .Select(unitID => unitID.GetFrom(world) ?? throw new NullReferenceException())
                .Where(unit => unit.CurrentOperation != null)
                .Select(unit => unit.CurrentOperation);
            var orderedOperations = new StartUnitsOrderer { Operations = operations }.Resolve(context);

            // 順番に命令を実行する
            foreach (var operation in orderedOperations)
            {
                var card = operation.CardID.GetFrom(world) ?? throw new NullReferenceException();
            }

            // 結果を返す

            throw new NotImplementedException();
        }
    }
}