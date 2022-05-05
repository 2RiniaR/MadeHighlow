using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行する際の、行動順を決定するクエリ
    /// </summary>
    public record ActuateUnitOrderer
    {
        public ValueObjectList<UnitEnsuredID> TargetsID { get; init; } = ValueObjectList<UnitEnsuredID>.Empty;

        [NotNull]
        public ValueObjectList<Unit> Resolve([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}