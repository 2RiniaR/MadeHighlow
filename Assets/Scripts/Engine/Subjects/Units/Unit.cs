using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public sealed record Unit(
        ID ID,
        bool IsActive,
        bool IsDead,
        [NotNull] [ItemNotNull] ValueList<Component> Components,
        [NotNull] Strength Strength,
        [NotNull] Medo Medo,
        Shadow Shadow,
        [NotNull] Figure Figure,
        [NotNull] PlayerID FollowingID,
        [NotNull] EntityID EntityID
    ) : IIdentified, IAttachable
    {
        public UnitID UnitID => new(ID);
        public IAttachableID AttachableID => UnitID;

        public IAttachable WithComponents(ValueList<Component> components)
        {
            throw new NotImplementedException();
        }
    }
}
