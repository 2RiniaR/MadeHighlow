using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreatePlayer.RegisterPlayer
{
    public interface IAction
    {
        ID AssignedID { get; init; }
        [NotNull] Player InitialProps { get; init; }
    }

    public record Action(ID AssignedID, [NotNull] Player InitialProps) : IAction;
}
