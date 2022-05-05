using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record BigBangAction : Action<BigBangResult>
    {
        [NotNull] public World World { get; init; } = new();


        [NotNull]
        public override BigBangResult Validate([NotNull] in IActionContext context)
        {
            if (!context.Session.Events.IsEmpty)
            {
                return BigBangResult.FailedByNotEmpty;
            }

            var generatedWorld = new WorldFormatter().Format(World);
            return new SucceedBigBangResult { GeneratedWorld = generatedWorld };
        }
    }
}