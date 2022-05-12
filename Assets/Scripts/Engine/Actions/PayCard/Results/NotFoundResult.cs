using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    /// <summary>
    ///     カードの支払いに失敗した結果
    /// </summary>
    public record NotFoundResult([NotNull] CardID TargetID) : PayCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
