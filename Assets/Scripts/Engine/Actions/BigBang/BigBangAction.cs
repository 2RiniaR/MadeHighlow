using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record BigBangAction : IValidatable
    {
        [NotNull] public World World { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public BigBangResult Validate([NotNull] in IActionContext context)
        {
            if (!context.Session.Events.IsEmpty) return BigBangResult.FailedByNotEmpty;

            var generatedWorld = new WorldFormatter().Format(World);
            return new SucceedBigBangResult { GeneratedWorld = generatedWorld };
        }
    }
}