using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public record Action(ID AssignedID, [NotNull] Player InitialProps);
}
