using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record GenerateEntityAction([NotNull] Entity InitialProps) : IValidAction;
}
