using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record DestroyedResult([NotNull] Action Action, [NotNull] Process Process) : Result;
}
