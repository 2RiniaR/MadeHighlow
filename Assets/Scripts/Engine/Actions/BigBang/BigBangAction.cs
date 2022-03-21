using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Events.BigBang;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Actions.BigBang
{
    public record BigBangAction() : Action(ActionType.BigBang)
    {
        [NotNull] public World World { get; init; } = new();

        public override EventTimeline Run(in Session session)
        {
            var generatedWorld = new WorldFormatter().Format(World);
            return new EventTimeline(new BigBangEvent { GeneratedWorld = generatedWorld });
        }
    }
}