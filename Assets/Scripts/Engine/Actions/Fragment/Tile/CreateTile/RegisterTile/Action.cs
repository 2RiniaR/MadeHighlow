using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile.RegisterTile
{
    public interface IAction
    {
        ID AssignedID { get; init; }
        [NotNull] Tile InitialProps { get; init; }
    }

    public record Action(ID AssignedID, [NotNull] Tile InitialProps) : IAction;
}
