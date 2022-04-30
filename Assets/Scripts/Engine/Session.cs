using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record Session([NotNull] ValueObjectList<SessionEvent> Events)
    {
        public Session(params SessionEvent[] events) : this(events.ToValueObjectList())
        {
        }
    }
}