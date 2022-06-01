using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     コンポーネント
    /// </summary>
    public abstract record Component(
        ID ID,
        [NotNull] IAttachableID AttachedID,
        [NotNull] Duration Duration
    ) : IIdentified, IComponent
    {
        public ComponentID ComponentID => new(ID);
    }
}
