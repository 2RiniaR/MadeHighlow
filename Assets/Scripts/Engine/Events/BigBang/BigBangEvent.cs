using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Events.BigBang
{
    public record BigBangEvent() : Event(EventType.BigBang)
    {
        [NotNull] public World GeneratedWorld { get; init; } = new();

        public override World Simulate(in World world)
        {
            return GeneratedWorld;
        }
    }
}