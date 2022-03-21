using System.Collections.Immutable;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Engine.Events
{
    public record EventTimeline([NotNull] [ItemNotNull] ImmutableList<Event> Events)
    {
        public EventTimeline(params Event[] events) : this(ImmutableList.Create(events))
        {
        }
    }
}