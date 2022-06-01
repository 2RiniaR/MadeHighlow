using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterEntity
{
    public record RegisterEntityAction(ID AssignedID, [NotNull] Entity InitialProps);
}
