using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity.RegisterEntity
{
    public interface IAction
    {
        ID AssignedID { get; init; }
        [NotNull] Entity InitialProps { get; init; }
    }

    public record Action(ID AssignedID, [NotNull] Entity InitialProps) : IAction;
}
