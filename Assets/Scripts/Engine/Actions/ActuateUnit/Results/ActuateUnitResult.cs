using System;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットが現在受けている命令を実行するアクションの結果
    /// </summary>
    public record ActuateUnitResult : ISimulatable
    {
        public World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}