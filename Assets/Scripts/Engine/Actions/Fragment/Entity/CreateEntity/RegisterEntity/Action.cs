using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateEntity.RegisterEntity
{
    public record Action(ID AssignedID, [NotNull] Entity InitialProps);
}
