using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public record SucceedResult([NotNull] Action Action, [NotNull] Card Registered) : Result;
}
