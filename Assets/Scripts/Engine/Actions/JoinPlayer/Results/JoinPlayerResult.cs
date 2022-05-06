using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerResult([NotNull] in Player JoinedPlayer) : Result
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}