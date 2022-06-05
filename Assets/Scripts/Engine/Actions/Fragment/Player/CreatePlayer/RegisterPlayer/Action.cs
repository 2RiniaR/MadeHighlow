using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer.RegisterPlayer
{
    public record Action(ID AssignedID, [NotNull] Player InitialProps);
}
