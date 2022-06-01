using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record CreateEntityAction([NotNull] Entity InitialProps);
}
