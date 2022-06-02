using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public record ParentNotFoundResult([NotNull] RegisterCardAction Action) : RegisterCardResult;
}
