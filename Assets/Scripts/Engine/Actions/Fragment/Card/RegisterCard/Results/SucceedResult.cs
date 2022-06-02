using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public record SucceedResult([NotNull] RegisterCardAction Action, [NotNull] Card Registered) : RegisterCardResult;
}
