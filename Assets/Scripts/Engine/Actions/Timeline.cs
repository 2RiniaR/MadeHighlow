using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public sealed record Timeline()
    {
        private Timeline([NotNull] [ItemNotNull] ValueList<Event> events) : this()
        {
            Events = events;
        }

        [NotNull] [ItemNotNull] public ValueList<Event> Events { get; }

        [NotNull]
        public Timeline Then([CanBeNull] Event @event)
        {
            if (@event == null) return this;
            return new Timeline(Events.Add(@event));
        }

        [NotNull]
        public Timeline Then([NotNull] [ItemNotNull] in ValueList<Event> events)
        {
            return new Timeline(Events.AddRange(events));
        }

        [NotNull]
        public Timeline Then<TEvent>([NotNull] [ItemNotNull] ValueList<TEvent> events) where TEvent : Event
        {
            return new Timeline(Events.AddRange(events));
        }

        [NotNull]
        public World Simulate([NotNull] World world)
        {
            return Events.Aggregate(world, (current, @event) => @event.Result.Simulate(current));
        }
    }
}
