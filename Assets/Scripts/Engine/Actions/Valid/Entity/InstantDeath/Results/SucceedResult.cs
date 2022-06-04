using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public sealed record SucceedResult([NotNull] Action Action) : Result;
}
