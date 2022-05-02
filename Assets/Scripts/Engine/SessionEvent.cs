using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public record SessionEvent
    {
        public ID<SessionEvent> ID { get; init; } = new();
        [NotNull] public Result Result { get; init; } = new EmptyResult();
    }
}