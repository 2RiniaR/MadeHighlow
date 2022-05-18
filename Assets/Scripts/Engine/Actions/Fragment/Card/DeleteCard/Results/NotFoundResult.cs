using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteCard
{
    public record NotFoundResult([NotNull] DeleteCardAction Action) : DeleteCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
