using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IValidAction
    {
        [NotNull]
        IValidResult Evaluate([NotNull] IActionRunner runner, [NotNull] IHistory history);
    }
}
