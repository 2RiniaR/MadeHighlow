using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record BigBangAction() : Action(ActionType.BigBang)
    {
        [NotNull] public World World { get; init; } = new();

        public BigBangResult Run(in Session session)
        {
            if (!session.Events.Items.IsEmpty) return BigBangResult.FailedByNotEmpty;

            var generatedWorld = new WorldFormatter().Format(World);
            return new SucceedBigBangResult { GeneratedWorld = generatedWorld };
        }
    }
}