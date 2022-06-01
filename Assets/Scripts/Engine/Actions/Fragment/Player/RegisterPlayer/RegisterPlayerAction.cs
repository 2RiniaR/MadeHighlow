using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public record RegisterPlayerAction(ID AssignedID, [NotNull] Player InitialProps);
}
