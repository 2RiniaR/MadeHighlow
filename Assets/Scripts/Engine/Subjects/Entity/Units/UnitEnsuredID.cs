using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public sealed record UnitEnsuredID : EntityEnsuredID
    {
        [CanBeNull]
        public new Unit GetFrom([NotNull] in World world)
        {
            return Entity.GetAllFrom(world).Find(entity => entity.EnsuredID == this) as Unit;
        }
    }
}