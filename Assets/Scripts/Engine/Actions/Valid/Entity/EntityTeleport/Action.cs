using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record Action([NotNull] EntityID TargetID, [NotNull] Position3D Destination) : IValidAction
    {
        public IValidResult Evaluate(IActionRunner runner, IHistory history)
        {
            return runner.EntityTeleport(history, this);
        }
    }
}
