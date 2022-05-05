using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行するアクションの結果
    /// </summary>
    public record StartUnitsResult : Result
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}