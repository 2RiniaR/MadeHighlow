using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record BigBangAction() : Action(ActionType.BigBang)
    {
        [NotNull] public World World { get; init; } = new();

        public BigBangEvent Run(in Session session)
        {
            if (!session.Events.Items.IsEmpty) return BigBangEvent.FailedByNotEmpty;

            var generatedWorld = new WorldFormatter().Format(World);
            return new SucceedBigBangEvent { GeneratedWorld = generatedWorld };
        }
    }
}