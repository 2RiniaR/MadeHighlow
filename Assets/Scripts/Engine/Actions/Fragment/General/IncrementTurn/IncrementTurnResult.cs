using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public record IncrementTurnResult([NotNull] Turn Updated) : Result
    {
        public override World Simulate(World world)
        {
            return world with { CurrentTurn = Updated };
        }
    }
}
