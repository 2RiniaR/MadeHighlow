using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを新規登録した結果
    /// </summary>
    public record FailedRegisterCardResult([NotNull] Card Card, FailedRegisterCardReason Reason) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
