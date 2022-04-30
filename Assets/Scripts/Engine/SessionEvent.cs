using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public record SessionEvent(
        ID<SessionEvent> ID,
        [NotNull] Event Event
    );
}