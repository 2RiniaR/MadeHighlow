using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine
{
    public record Session(
        [NotNull] [ItemNotNull] ImmutableList<SessionEvent> Events
    )
    {
        public Session(params SessionEvent[] events) : this(ImmutableList.Create(events))
        {
        }
    }
}