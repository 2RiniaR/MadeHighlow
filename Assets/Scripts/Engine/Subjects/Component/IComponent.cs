using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IComponent
    {
        [NotNull] public IAttachableID AttachedID { get; }
        [NotNull] public ComponentID ComponentID { get; }
    }
}