using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterEntity
{
    public record Action(ID AssignedID, [NotNull] Entity InitialProps);
}
