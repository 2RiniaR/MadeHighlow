using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public interface IAction
    {
        [NotNull] Entity InitialProps { get; init; }
    }

    public record Action([NotNull] Entity InitialProps) : IAction;
}
