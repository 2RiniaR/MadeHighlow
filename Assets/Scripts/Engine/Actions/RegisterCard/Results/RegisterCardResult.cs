using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを新規登録した結果
    /// </summary>
    public record RegisterCardResult([NotNull] Card RegisteredCard) : Result
    {
        public override World Simulate(World world)
        {
            return RegisteredCard.CreateIn(world);
        }
    }
}