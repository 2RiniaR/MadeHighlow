using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
