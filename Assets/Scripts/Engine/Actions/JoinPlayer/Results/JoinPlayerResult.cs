using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerResult([NotNull] Player JoinedPlayer) : Result
    {
        public override World Simulate(World world)
        {
            throw new NotImplementedException();
        }
    }
}