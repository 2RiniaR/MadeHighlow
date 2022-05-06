using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public sealed record UnitID(ID Content) : EntityID(Content)
    {
        [CanBeNull]
        public new Unit GetFrom([NotNull] in World world)
        {
            return Entity.GetAllFrom(world).Find(entity => entity.EntityID == this) as Unit;
        }
    }
}