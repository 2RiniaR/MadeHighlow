using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     複数のユニットが一斉に現在受けている命令を実行するアクション
    /// </summary>
    public record StartUnitsAction : Action<StartUnitsResult>
    {
        /// <summary>
        ///     実行を命令するユニット
        /// </summary>
        [NotNull]
        public ValueObjectList<UnitEnsuredID> TargetsID { get; init; } = ValueObjectList<UnitEnsuredID>.Empty;

        public override StartUnitsResult Validate(in IActionContext context)
        {
            return new StartUnitsResult
            {
                Operations = RunOperations(context),
            };
        }

        [NotNull]
        [ItemNotNull]
        private ValueObjectList<RunOperationResult> RunOperations([NotNull] in IActionContext context)
        {
            var currentContext = context;
            var operationResults = new List<RunOperationResult>();

            foreach (var operation in OrderedOperations(context))
            {
                var action = new RunOperationAction { Operation = operation };
                var result = action.Validate(context);
                currentContext = currentContext.Appended(result);
                operationResults.Add(result);
            }

            return operationResults.ToValueObjectList();
        }

        [NotNull]
        [ItemNotNull]
        private ValueObjectList<UnitOperation> OrderedOperations([NotNull] in IActionContext context)
        {
            var world = context.World;
            var operations = TargetsID
                .Select(unitID => unitID.GetFrom(world) ?? throw new NullReferenceException())
                .Where(unit => unit.CurrentOperation != null)
                .Select(unit => unit.CurrentOperation);
            return new StartUnitsOrderer { Operations = operations }.Resolve(context);
        }
    }
}