using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public sealed record SucceedResult([NotNull] Action Action) : Result;
}
