using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IComponent
    {
        [NotNull] public IAttachableEnsuredID AttachedID { get; }
        [NotNull] public ComponentEnsuredID EnsuredID { get; }
    }
}