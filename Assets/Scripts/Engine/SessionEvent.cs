using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;

namespace RineaR.MadeHighlow.Engine
{
    public record SessionEvent(
        ID<SessionEvent> ID,
        [NotNull] Event Event
    );
}